using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0183_Seagulls : SetObjectHeroes
    {
        public byte Number { get; set; }
        public float Radius { get; set; }
        public float Speed { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Number = reader.ReadByte();
            reader.BaseStream.Position += 3;
            Radius = reader.ReadSingle();
            Speed = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Number);
            writer.Pad(3);
            writer.Write(Radius);
            writer.Write(Speed);
        }
    }
}
