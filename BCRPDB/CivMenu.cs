﻿using BCRPDB.Properties;
using MaterialSkin;
using MaterialSkin.Controls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#pragma warning disable IDE1006

namespace BCRPDB
{
    public partial class CivMenu : MaterialForm
    {
        Socket client;
        Civ localCiv;
        public ushort ID = 0;
        public bool closed = false;
        bool downloaded = false;
        byte downloadedTimeout = 0;
        Config cfg;

        public CivMenu(ushort ID)
        {
            cfg = new Config("settings.ini");

            client = new Socket(SocketType.Stream, ProtocolType.Tcp);
            this.ID = ID;

            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Green500, Primary.Green900, Primary.Green300, Accent.Cyan400, TextShade.WHITE);
        }

        private void addWep_Click(object sender, EventArgs e)
        {
            RegWeaponMenu menu = new RegWeaponMenu();

            menu.ShowDialog();

            regWepList.Items.Add(menu.WeaponName.Text);
        }

        private void remWep_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in regWepList.SelectedItems)
                item.Remove();

            sync.Checked = false;
        }

        private bool RefreshCiv()
        {
            try
            {
                client.Connect(cfg.IP, cfg.Port);
            }
            catch (SocketException)
            {
                return false;
            }

            client.Send(new byte[] { 0 }.Concat(BitConverter.GetBytes(ID)).ToArray());

            byte[] b = new byte[1001];
            int e = client.Receive(b);
            byte tag = b[0];
            b = b.Skip(1).ToArray();
            e = e - 1;

            client.Disconnect(true);
            client = new Socket(SocketType.Stream, ProtocolType.Tcp);

            switch (tag)
            {
                case 0:
                    localCiv = Civ.ToCiv(b.Take(e).ToArray());
                    ID = localCiv.CivID;
                    break;

                case 1:
                    if (!downloaded)
                        downloadedTimeout++;
                    return false;
            }

            return true;
        }

        private bool UpdateCiv()
        {
            try
            {
                client.Connect(cfg.IP, cfg.Port);
            }
            catch (SocketException)
            {
                return false;
            }

            client.Send(new byte[] { 1 }.Concat(localCiv.ToBytes()).ToArray());

            client.Disconnect(true);
            client = new Socket(SocketType.Stream, ProtocolType.Tcp);

            return true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            downloaded = downloaded || downloadedTimeout > 2;

            if (!sync.Checked)
                Sync(downloaded);
            else
                Sync(false);
        }

        private void name_TextChanged(object sender, EventArgs e) =>
            sync.Checked = false;

        private void business_TextChanged(object sender, EventArgs e) =>
            sync.Checked = false;

        private void plate_TextChanged(object sender, EventArgs e) =>
            sync.Checked = false;

        private void syncBtn_Click(object sender, EventArgs e) =>
            Sync(downloaded);

        public void Sync(bool update = true)
        {
            ThreadPool.QueueUserWorkItem(z =>
            {
                if (localCiv == null)
                    localCiv = new Civ(ID);

                int[] nameS = new int[2];
                int[] plateS = new int[2];
                int[] businessS = new int[2];

                bool regWepB;
                int[] regWepS = new int[0];

                bool ticketB;
                int ticketS = 0;

                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        regWepB = regWepList.SelectedItems.Count != 0;
                        regWepS = new int[regWepList.SelectedItems.Count];

                        ticketB = ticketList.SelectedItems.Count != 0;

                        localCiv.Name = name.Text;
                        nameS[0] = name.SelectionStart;
                        nameS[1] = name.SelectionLength;

                        localCiv.RegisteredPlate = plate.Text;
                        plateS[0] = plate.SelectionStart;
                        plateS[1] = plate.SelectionLength;
                        
                        localCiv.AssociatedBusiness = business.Text;
                        businessS[0] = business.SelectionStart;
                        businessS[1] = business.SelectionLength;

                        localCiv.RegisteredWeapons.Clear();
                        foreach (ListViewItem item in regWepList.Items)
                            localCiv.RegisteredWeapons.Add(item.Text);

                        if (regWepB)
                            for (int i = 0; i < regWepS.Length; i++)
                                regWepS[i] = regWepList.SelectedItems[i].Index;

                        if (ticketB)
                            ticketS = ticketList.SelectedItems[0].Index;
                    });
                }
                catch { return; }
                
                if (update)
                    if (!UpdateCiv())
                        return;
                if (!RefreshCiv())
                    return;

                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        idDisp.Text = "Your civilian ID: " + localCiv.CivID.ToString();
                        name.Text = localCiv.Name;
                        if (name.Text.Length != 0)
                        {
                            name.SelectionStart = nameS[0];
                            name.SelectionLength = nameS[1];
                        }

                        plate.Text = localCiv.RegisteredPlate;
                        if (plate.Text.Length != 0)
                        {
                            plate.SelectionStart = plateS[0];
                            plate.SelectionLength = plateS[1];
                        }

                        business.Text = localCiv.AssociatedBusiness;
                        if (business.Text.Length != 0)
                        {
                            business.SelectionStart = businessS[0];
                            business.SelectionLength = businessS[1];
                        }

                        ticketList.Items.Clear();
                        localCiv.Tickets.ForEach(x => ticketList.Items.Add(new ListViewItem(new string[] { x.Key, x.Value })));
                        if (ticketList.Items.Count != 0)
                            ticketList.Items[ticketS].Selected = true;

                        regWepList.Items.Clear();
                        localCiv.RegisteredWeapons.ForEach(x => regWepList.Items.Add(x));
                        if (regWepList.Items.Count != 0)
                            foreach (int i in regWepS)
                                regWepList.Items[i].Selected = true;

                        sync.Checked = true;
                    });
                }
                catch { }

                downloaded = true;
            });
        }

        private void CivMenu_FormClosed(object sender, FormClosedEventArgs e) =>
            closed = true;

        private void CivMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!sync.Checked && MessageBox.Show("Your civilian is not synced to the server.\nIf you exit your civilian will not be saved!", "BCRPDB", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void delete_Click(object sender, EventArgs ev)
        {
            try
            {
                client.Connect(cfg.IP, cfg.Port);
            }
            catch (SocketException)
            {
                return;
            }

            client.Send(new byte[] { 4 }.Concat(BitConverter.GetBytes(ID)).ToArray());

            byte[] b = new byte[1001];
            int e = client.Receive(b);
            byte tag = b[0];
            b = b.Skip(1).ToArray();
            e = e - 1;

            client.Disconnect(true);
            client = new Socket(SocketType.Stream, ProtocolType.Tcp);

            switch (tag)
            {
                case 0:
                    localCiv = Civ.ToCiv(b.Take(e).ToArray());
                    Sync(false);
                    break;

                case 1:
                    MessageBox.Show("Your civilian was not able to be deleted. This is most likely an error in reserving civs.", "BCRPDB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
    }
}
