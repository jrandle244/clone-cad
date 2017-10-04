using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CloneCAD.Common.DataHolders
{
    public class StorableValue<T>
    {
        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                valChanged = true;
                _value = value;
            }
        }

        public string FilePath { get; set; }

        private byte[] bytes;
        private bool valChanged;
        public byte[] Bytes
        {
            get
            {
                if (valChanged)
                    return bytes;

                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    bf.Serialize(ms, Value);
                    bytes = ms.ToArray();
                }

                valChanged = false;
                return bytes;
            }
        }

        public StorableValue(T Value) =>
            this.Value = Value;

        public StorableValue(string FilePath)
        {
            if (!File.Exists(FilePath))
                throw new InvalidOperationException("The file does not exist!");

            this.FilePath = FilePath;

            LoadValueFromBytes(File.ReadAllBytes(FilePath));
        }

        public StorableValue(byte[] ValueBytes)
        {
            LoadValueFromBytes(ValueBytes);
        }

        public override string ToString() => Value.ToString();
        public override bool Equals(object obj) => Value.Equals(obj);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(StorableValue<T> obj1, T obj2) =>
            obj1.Value.Equals(obj2);

        public static bool operator !=(StorableValue<T> obj1, T obj2) =>
            !obj1.Value.Equals(obj2);

        public void LoadValueFromBytes(byte[] ValueBytes)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(ValueBytes, 0, ValueBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                Value = (T)bf.Deserialize(memStream);
            }
        }

        public void Save()
        {
            if (string.IsNullOrWhiteSpace(FilePath))
                throw new InvalidOperationException("Cannot save to file if " + nameof(FilePath) + " is null or whitespace!");

            File.WriteAllBytes(FilePath, Bytes);
        }
    }
}
