using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object050A_Dice : SetObjectHeroes
    {
        public enum EDiceType : short
        {
            UpDown = 0,
            Horizontal = 1
        }

        public float Speed { get; set; }
        public float Height { get; set; }
        public float Radius { get; set; }
        public short StopTime { get; set; }
        public EDiceType DiceType { get; set; }
        public short OffsetTime { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Speed = reader.ReadSingle();
            Height = reader.ReadSingle();
            Radius = reader.ReadSingle();
            StopTime = reader.ReadInt16();
            DiceType = (EDiceType)reader.ReadInt16();
            OffsetTime = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Speed);
            writer.Write(Height);
            writer.Write(Radius);
            writer.Write(StopTime);
            writer.Write((short)DiceType);
            writer.Write(OffsetTime);
        }
    }
}