using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0502_Flipper : SetObjectHeroes
    {
        public float Trance { get; set; }
        public byte FlipperType { get; set; }
        public byte KeyFlip { get; set; }
        public byte Power { get; set; }
        public byte Player { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Trance = reader.ReadSingle();
            FlipperType = reader.ReadByte();
            KeyFlip = reader.ReadByte();
            Power = reader.ReadByte();
            Player = reader.ReadByte();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Trance);
            writer.Write(FlipperType);
            writer.Write(KeyFlip);
            writer.Write(Power);
            writer.Write(Player);
        }
    }
}