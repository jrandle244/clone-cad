﻿using CloneCAD.Common.DataHolders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CloneCAD.Server.DataHolders.Static
{
    [Serializable]
    public class CivilianDictionary : Dictionary<uint, Civilian>
    {
        public CivilianDictionary() { }
        public CivilianDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public bool ContainsName(string name) =>
            Keys.Any(x => this[x].Name == name);

        public bool ContainsPlate(string plate) =>
            Keys.Any(x => this[x].RegisteredPlate == plate);

        public IEnumerable<T> Select<T>(Func<KeyValuePair<uint, Civilian>, T> selector) =>
             Keys.Select(id => selector(new KeyValuePair<uint, Civilian>(id, base[id]))).ToList();

        public Civilian this[string name]
        {
            get
            {
                if (!ContainsName(name))
                    throw new InvalidOperationException($"Cannot get {nameof(Civilian)} by name when no {nameof(Civilian)} has this name.");

                return Keys.Where(x => base[x].Name == name).Select(x => base[x]).FirstOrDefault();
            }
            set
            {
                if (!ContainsName(name))
                    throw new InvalidOperationException($"Cannot set {nameof(Civilian)} by name when no {nameof(Civilian)} has this name.");

                foreach (uint id in Keys)
                    if (base[id].Name == name)
                        base[id] = value;
            }
        }

        public void Add(Civilian civilian) =>
            Add(civilian.ID, civilian);

        public Civilian GetFromPlate(string plate)
        {
            if (!ContainsPlate(plate))
                throw new InvalidOperationException($"Cannot get {nameof(Civilian)} by plate when no {nameof(Civilian)} has this plate.");

            return Keys.Where(x => base[x].RegisteredPlate == plate).Select(x => base[x]).FirstOrDefault();
        }

        public void SetFromPlate(string plate, Civilian civ)
        {
            if (!ContainsName(plate))
                throw new InvalidOperationException($"Cannot set {nameof(Civilian)} by plate when no {nameof(Civilian)} has this plate.");

            foreach (uint id in Keys)
                if (base[id].RegisteredPlate == plate)
                    base[id] = civ;
        }
    }
}
