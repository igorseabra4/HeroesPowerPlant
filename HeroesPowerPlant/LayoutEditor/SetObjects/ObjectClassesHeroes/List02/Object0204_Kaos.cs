using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0204_Kaos : SetObjectHeroes
    {
        public byte KaosNumber { get; set; }
        public float MinSpeed { get; set; }
        public float MaxSpeed { get; set; }
        public float Acceleration { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            KaosNumber = reader.ReadByte();
            reader.BaseStream.Position += 3;
            MinSpeed = reader.ReadSingle();
            MaxSpeed = reader.ReadSingle();
            Acceleration = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(KaosNumber);
            writer.Pad(3);
            writer.Write(MinSpeed);
            writer.Write(MaxSpeed);
            writer.Write(Acceleration);
        }
    }
}
