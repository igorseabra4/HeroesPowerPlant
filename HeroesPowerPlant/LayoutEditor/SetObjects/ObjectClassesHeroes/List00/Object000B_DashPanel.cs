using HeroesPowerPlant.Shared.Utilities;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000B_DashPanel : SetObjectHeroes
    {
        [Description("Defaults to 5.0")]
        public float Speed { get; set; }
        [Description("In frames")]
        public short NoControlTime { get; set; }
        [Description("Usually 0")]
        public float Unknown1 { get; set; }
        [Description("Usually 0")]
        public float Unknown2 { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Speed = reader.ReadSingle();
            NoControlTime = reader.ReadInt16();
            reader.BaseStream.Position += 2;
            Unknown1 = reader.ReadSingle();
            reader.BaseStream.Position += 8;
            Unknown2 = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Speed);
            writer.Write(NoControlTime);
            writer.Pad(2);
            writer.Write(Unknown1);
            writer.Pad(8);
            writer.Write(Unknown2);
        }
    }
}