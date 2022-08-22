using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0105_MovingRuin : SetObjectHeroes
    {
        public enum ERuinType : byte
        {
            Small = 0,
            Normal = 1,
            Special = 2
        }

        public ERuinType RuinType { get; set; }
        public float StartY { get; set; }
        public float Speed { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            RuinType = (ERuinType)reader.ReadByte();
            reader.BaseStream.Position += 3;
            StartY = reader.ReadSingle();
            Speed = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)RuinType);
            writer.Pad(3);
            writer.Write(StartY);
            writer.Write(Speed);
        }
    }
}
