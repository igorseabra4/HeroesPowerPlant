using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_16_00_01 : SetObjectHeroes
    {
        public float Radius { get; set; }
        public int Refresh { get; set; }
        public int Disable { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Radius = reader.ReadSingle();
            Refresh = reader.ReadInt32();
            Disable = reader.ReadInt32();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Radius);
            writer.Write(Refresh);
            writer.Write(Disable);
        }
    }
}
