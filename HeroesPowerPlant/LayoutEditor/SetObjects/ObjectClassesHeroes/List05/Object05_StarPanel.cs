using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object05_StarPanel : SetObjectHeroes
    {
        public float Power { get; set; }
        public int Color { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Power = reader.ReadSingle();
            Color = reader.ReadInt32();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Power);
            writer.Write(Color);
        }
    }
}