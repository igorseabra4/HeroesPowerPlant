using HeroesPowerPlant.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_HeroesDefault : SetObjectHeroes
    {
        private byte[] _miscSettingBytes = new byte[byteCount];
        private const int byteCount = 32;

        public override void ReadMiscSettings(BinaryReader reader)
        {
            _miscSettingBytes = reader.ReadBytes(byteCount);
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write(_miscSettingBytes);
        }

        public byte[] MiscSettingBytes
        {
            get => _miscSettingBytes;
            set
            {
                var result = value.ToList();

                while (result.Count < byteCount)
                    result.Add(0);
                while (result.Count > byteCount)
                    result.RemoveAt(result.Count - 1);

                _miscSettingBytes = result.ToArray();
            }
        }

        public short[] MiscSettingShorts
        {
            get
            {
                var reader = new EndianBinaryReader(new MemoryStream(_miscSettingBytes), Endianness.Big);
                var result = new List<short>(byteCount / 2);
                while (!reader.EndOfStream)
                    result.Add(reader.ReadInt16());
                return result.ToArray();
            }
            set
            {
                var result = new List<byte>();
                foreach (short i in value)
                    result.AddRange(BitConverter.GetBytes(i).Reverse());
                MiscSettingBytes = result.ToArray();
            }
        }

        public int[] MiscSettingInts
        {
            get
            {
                var reader = new EndianBinaryReader(new MemoryStream(_miscSettingBytes), Endianness.Big);
                var result = new List<int>(byteCount / 4);
                while (!reader.EndOfStream)
                    result.Add(reader.ReadInt32());
                return result.ToArray();
            }
            set
            {
                var result = new List<byte>();
                foreach (int i in value)
                    result.AddRange(BitConverter.GetBytes(i).Reverse());
                MiscSettingBytes = result.ToArray();
            }
        }

        public float[] MiscSettingFloats
        {
            get
            {
                var reader = new EndianBinaryReader(new MemoryStream(_miscSettingBytes), Endianness.Big);
                var result = new List<float>(byteCount / 4);
                while (!reader.EndOfStream)
                    result.Add(reader.ReadSingle());
                return result.ToArray();
            }
            set
            {
                var result = new List<byte>();
                foreach (int i in value)
                    result.AddRange(BitConverter.GetBytes(i).Reverse());
                MiscSettingBytes = result.ToArray();
            }
        }
    }
}