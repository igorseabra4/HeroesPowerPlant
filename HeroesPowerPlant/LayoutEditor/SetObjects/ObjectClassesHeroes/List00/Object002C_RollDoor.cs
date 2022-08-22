using HeroesPowerPlant.Shared.Utilities;
using System;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object002C_RollDoor : SetObjectHeroes
    {
        [Description("Defaults to 5.0")]
        public float Power { get; set; }
        [Description("In degrees")]
        public float Elevation { get; set; }
        [Description("In frames")]
        public short NoControlTime { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Power = reader.ReadSingle();
            Elevation = reader.ReadSingle();
            NoControlTime = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Power);
            writer.Write(Elevation);
            writer.Write(NoControlTime);
        }
    }
}