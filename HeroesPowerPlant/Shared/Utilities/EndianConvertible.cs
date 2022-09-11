using System;
using System.IO;
using System.Linq;

namespace HeroesPowerPlant.Shared.Utilities
{
    public enum Endianness
    {
        Little,
        Big,
        Unknown
    }

    public class EndianBinaryReader : BinaryReader
    {
        public readonly Endianness endianness;

        public EndianBinaryReader(Stream stream, Endianness endianness) : base(stream)
        {
            this.endianness = endianness;
        }

        public override float ReadSingle() =>
            (endianness == Endianness.Little) ?
            base.ReadSingle() :
            BitConverter.ToSingle(base.ReadBytes(4).Reverse().ToArray(), 0);

        public override short ReadInt16() =>
            (endianness == Endianness.Little) ?
            base.ReadInt16() :
            BitConverter.ToInt16(base.ReadBytes(2).Reverse().ToArray(), 0);

        public override int ReadInt32() =>
            (endianness == Endianness.Little) ?
            base.ReadInt32() :
            BitConverter.ToInt32(base.ReadBytes(4).Reverse().ToArray(), 0);

        public override ushort ReadUInt16() =>
            (endianness == Endianness.Little) ?
            base.ReadUInt16() :
            BitConverter.ToUInt16(base.ReadBytes(2).Reverse().ToArray(), 0);

        public override uint ReadUInt32() =>
            (endianness == Endianness.Little) ?
            base.ReadUInt32() :
            BitConverter.ToUInt32(base.ReadBytes(4).Reverse().ToArray(), 0);

        public bool ReadByteBool() => ReadByte() != 0;

        public bool ReadInt16Bool() => ReadInt16() != 0;

        public bool ReadInt32Bool() => ReadInt32() != 0;

        public string ReadString(int length) => System.Text.Encoding.GetEncoding(1252).GetString(ReadBytes(length));

        public bool EndOfStream => BaseStream.Position == BaseStream.Length;
    }

    public class EndianBinaryWriter : BinaryWriter
    {
        public readonly Endianness endianness;

        public EndianBinaryWriter(Stream stream, Endianness endianness) : base(stream)
        {
            this.endianness = endianness;
        }

        public override void Write(float f)
        {
            if (endianness == Endianness.Little)
                base.Write(f);
            else
                WriteReverse(BitConverter.GetBytes(f));
        }

        public override void Write(int f)
        {
            if (endianness == Endianness.Little)
                base.Write(f);
            else
                WriteReverse(BitConverter.GetBytes(f));
        }

        public override void Write(short f)
        {
            if (endianness == Endianness.Little)
                base.Write(f);
            else
                WriteReverse(BitConverter.GetBytes(f));
        }

        public override void Write(uint f)
        {
            if (endianness == Endianness.Little)
                base.Write(f);
            else
                WriteReverse(BitConverter.GetBytes(f));
        }

        public override void Write(ushort f)
        {
            if (endianness == Endianness.Little)
                base.Write(f);
            else
                WriteReverse(BitConverter.GetBytes(f));
        }

        public void Pad(int count)
        {
            Pad(0, count);
        }

        public void Pad(byte with, int count)
        {
            for (int i = 0; i < count; i++)
                Write(with);
        }

        private void WriteReverse(byte[] bytes) => base.Write(bytes.Reverse().ToArray());

        public override void Write(string f)
        {
            foreach (byte c in System.Text.Encoding.GetEncoding(1252).GetBytes(f))
                Write(c);
        }

        public void WriteMagic(string magic)
        {
            if (magic.Length != 4)
                throw new ArgumentException("Magic word must have 4 characters");
            var chars = magic.ToCharArray();
            Write(endianness == Endianness.Little ? chars : chars.Reverse().ToArray());
        }
    }
}
