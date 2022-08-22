using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0503_TriBumper : SetObjectHeroes
    {
        public float Power { get; set; }
        public byte AngType { get; set; }
        public byte Color { get; set; }
        public byte Number { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Power = reader.ReadSingle();
            AngType = reader.ReadByte();
            Color = reader.ReadByte();
            Number = reader.ReadByte();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Power);
            writer.Write(AngType);
            writer.Write(Color);
            writer.Write(Number);
        }
    }
}