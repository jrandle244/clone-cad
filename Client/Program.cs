using System;
using System.Windows.Forms;
using CloneCAD.Client;
using CloneCAD.Client.Menus;

namespace Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if !DEBUG
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
            catch (Exception e)
            {
                Functions.ExceptionHandler(e, 4);
            }
#endif
#if DEBUG
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
#endif
        }
    }
}
