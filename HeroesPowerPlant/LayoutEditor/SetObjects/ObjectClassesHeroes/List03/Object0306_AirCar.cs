using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0306_AirCar : SetObjectHeroes
    {
        public byte FarType { get; set; }
        public byte BlockType { get; set; }
        public byte CrossWize { get; set; }
        public byte LengthWize { get; set; }
        public short Time { get; set; }
        public short TimeRnd { get; set; }
        public float Length { get; set; }
        public float Speed { get; set; }
        public float SpeedRnd { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            FarType = reader.ReadByte();
            BlockType = reader.ReadByte();
            CrossWize = reader.ReadByte();
            LengthWize = reader.ReadByte();
            Time = reader.ReadInt16();
            TimeRnd = reader.ReadInt16();
            Length = reader.ReadSingle();
            Speed = reader.ReadSingle();
            SpeedRnd = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(FarType);
            writer.Write(BlockType);
            writer.Write(CrossWize);
            writer.Write(LengthWize);
            writer.Write(Time);
            writer.Write(TimeRnd);
            writer.Write(Length);
            writer.Write(Speed);
            writer.Write(SpeedRnd);
        }
    }
}