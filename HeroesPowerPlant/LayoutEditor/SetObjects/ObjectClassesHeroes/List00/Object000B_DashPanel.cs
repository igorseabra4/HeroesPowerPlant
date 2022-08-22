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

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Speed = reader.ReadSingle();
            NoControlTime = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Speed);
            writer.Write(NoControlTime);
        }
    }
}