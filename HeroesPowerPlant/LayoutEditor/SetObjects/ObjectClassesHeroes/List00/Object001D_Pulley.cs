using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object001D_Pulley : SetObjectHeroes
    {
        public enum EPulleyType : short
        {
            Up = 0,
            Down = 1
        }

        public float Elevation { get; set; }
        public float ElevationAngle { get; set; }
        public float Power { get; set; }
        public EPulleyType PulleyType { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Elevation = reader.ReadSingle();
            ElevationAngle = reader.ReadSingle();
            Power = reader.ReadSingle();
            PulleyType = (EPulleyType)reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Elevation);
            writer.Write(ElevationAngle);
            writer.Write(Power);
            writer.Write((short)(PulleyType));
        }
    }
}