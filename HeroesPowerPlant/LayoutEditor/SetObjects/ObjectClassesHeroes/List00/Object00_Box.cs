using HeroesPowerPlant.Shared.Utilities;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object00_Box : SetObjectHeroes
    {
        public enum ECrashMode : short
        {
            CrashOut = 0,
            CrashThrough = 1
        }

        public ECrashMode CrashMode { get; set; }
        [Description("Usually 0")]
        public short Unknown1 { get; set; }
        [Description("Usually 0")]
        public short Unknown2 { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            CrashMode = (ECrashMode)reader.ReadInt16();
            Unknown1 = reader.ReadInt16();
            Unknown2 = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((short)CrashMode);
            writer.Write(Unknown1);
            writer.Write(Unknown2);
        }
    }
}