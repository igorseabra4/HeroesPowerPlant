using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0401_EnergyColumn : SetObjectHeroes
    {
        public byte EnergyColumnType { get; set; }
        public float Length { get; set; }
        public float Speed { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            EnergyColumnType = reader.ReadByte();
            reader.BaseStream.Position += 3;
            Length = reader.ReadSingle();
            Speed = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(EnergyColumnType);
            writer.Pad(3);
            writer.Write(Length);
            writer.Write(Speed);
        }
    }
}
