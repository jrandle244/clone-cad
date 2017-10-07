using CloneCAD.Common.DataHolders;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CloneCAD.Server.DataHolders.Static
{
    public class CivilianDictionary : Dictionary<uint, Civilian>
    {
        public bool ContainsName(string name) =>
            Any(x => x.Value.Name == name);

        public bool ContainsPlate(string plate) =>
            Any(x => x.Value.RegisteredPlate == plate);

        public IEnumerable<T> Select<T>(Func<KeyValuePair<uint, Civilian>, T> selector) =>
             Keys.Select(id => selector(new KeyValuePair<uint, Civilian>(id, base[id]))).ToList();

        public Civilian this[string name]
        {
            get
            {
                if (!ContainsName(name))
                    throw new InvalidOperationException("Cannot get " + nameof(Civilian) + " by name when no " + nameof(Civilian) + " has this name.");

                return Keys.Where(x => base[x].Name == name).Select(x => base[x]).FirstOrDefault();
            }
            set
            {
                if (!ContainsName(name))
                    throw new InvalidOperationException("Cannot set " + nameof(Civilian) + " by name when no " + nameof(Civilian) + " has this name.");

                foreach (uint id in Keys)
                    if (base[id].Name == name)
                        base[id] = value;
            }
        }

        public void Add(Civilian civilian) =>
            Add(civilian.ID, civilian);

        public Civilian GetFromPlate(string plate)
        {
            if (!ContainsName(plate))
                throw new InvalidOperationException("Cannot get " + nameof(Civilian) + " by plate when no " + nameof(Civilian) + " has this plate.");

            return Keys.Where(x => base[x].RegisteredPlate == plate).Select(x => base[x]).FirstOrDefault();
        }

        public void SetFromPlate(string plate, Civilian civ)
        {
            if (!ContainsName(plate))
                throw new InvalidOperationException("Cannot get " + nameof(Civilian) + " by plate when no " + nameof(Civilian) + " has this plate.");

            foreach (uint id in Keys)
                if (base[id].RegisteredPlate == plate)
                    base[id] = civ;
        }

        private bool Any(Func<KeyValuePair<ushort, Civilian>, bool> predicate)
        {
            foreach (ushort id in Keys)
                return predicate(new KeyValuePair<ushort, Civilian>(id, base[id]));

            return false;
        }
    }
}
