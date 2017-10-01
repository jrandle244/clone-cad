using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class CivilianDictionary
    {
        private List<Civ> civs;

        public CivilianDictionary()
        {
            civs = new List<Civ>();
        }

        public void Add(Civ civ) =>
            civs.Add(civ);

        public void Remove(Civ civ) =>
            civs.Remove(civ);

        public void RemoveAt(int index) =>
            civs.RemoveAt(index);

        public bool ContainsID(ushort id) =>
            civs.Any(x => x.CivID == id);

        public bool ContainsName(string name) =>
            civs.Any(x => x.Name == name);

        public bool ContainsPlate(string plate) =>
            civs.Any(x => x.RegisteredPlate == plate);

        public IEnumerable<T> Select<T>(Func<Civ, T> selector) =>
            civs.Select(selector);

        public Civ this[ushort id]
        {
            get => civs.Find(x => x.CivID == id);
            set
            {
                Civ civ = civs.Find(x => x.CivID == id);

                if (civ == null)
                    civs.Add(value);
                else
                    civs[civs.IndexOf(civ)] = value;
            }
        }

        public Civ this[string name]
        {
            get => civs.Find(x => x.Name == name);
            set
            {
                Civ civ = civs.Find(x => x.Name == name);

                if (civ == null)
                    civs.Add(value);
                else
                    civs[civs.IndexOf(civ)] = value;
            }
        }

        public Civ GetFromPlate(string plate)
        {
            Civ civ = civs.Find(x => x.RegisteredPlate == plate);

            return civ;
        }

        public void SetFromPlate(string plate, Civ civ)
        {
            Civ oldCiv = civs.Find(x => x.RegisteredPlate == plate);

            civs[civs.IndexOf(oldCiv)] = civ ?? throw new ArgumentException("No civilian with that plate exists.", nameof(plate));
        }
    }
}
