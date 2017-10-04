using CloneCAD.Common.DataHolders;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CloneCAD.Server.DataHolders.Static
{
    public class CivilianDictionary : Dictionary<ushort, Civ>
    {
        public bool ContainsName(string name) =>
            Any(x => x.Value.Name == name);

        public bool ContainsPlate(string plate) =>
            Any(x => x.Value.RegisteredPlate == plate);

        public IEnumerable<T> Select<T>(Func<KeyValuePair<ushort, Civ>, T> Selector) =>
             Keys.Select(id => Selector(new KeyValuePair<ushort, Civ>(id, base[id]))).ToList();

        public Civ this[string name]
        {
            get
            {
                if (!ContainsName(name))
                    throw new InvalidOperationException("Cannot get " + nameof(Civ) + " by name when no " + nameof(Civ) + " has this name.");

                return Keys.Where(x => base[x].Name == name).Select(x => base[x]).FirstOrDefault();
            }
            set
            {
                if (!ContainsName(name))
                    throw new InvalidOperationException("Cannot set " + nameof(Civ) + " by name when no " + nameof(Civ) + " has this name.");

                foreach (ushort id in Keys)
                    if (base[id].Name == name)
                        base[id] = value;
            }
        }

        public Civ GetFromPlate(string plate)
        {
            if (!ContainsName(plate))
                throw new InvalidOperationException("Cannot get " + nameof(Civ) + " by plate when no " + nameof(Civ) + " has this plate.");

            return Keys.Where(x => base[x].RegisteredPlate == plate).Select(x => base[x]).FirstOrDefault();
        }

        public void SetFromPlate(string plate, Civ civ)
        {
            if (!ContainsName(plate))
                throw new InvalidOperationException("Cannot get " + nameof(Civ) + " by plate when no " + nameof(Civ) + " has this plate.");

            foreach (ushort id in Keys)
                if (base[id].RegisteredPlate == plate)
                    base[id] = civ;
        }

        private bool Any(Func<KeyValuePair<ushort, Civ>, bool> Predicate)
        {
            foreach (ushort id in Keys)
                return Predicate(new KeyValuePair<ushort, Civ>(id, base[id]));

            return false;
        }
    }
}
