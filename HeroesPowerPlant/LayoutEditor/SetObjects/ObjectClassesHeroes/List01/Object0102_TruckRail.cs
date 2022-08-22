using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0102_TruckRail : SetObjectHeroes
    {
        public byte ObjectType { get; set; }
        public byte StopTime { get; set; }
        public float Length { get; set; }
        public float Speed { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            ObjectType = reader.ReadByte();
            StopTime = reader.ReadByte();
            reader.BaseStream.Position += 2;
            Length = reader.ReadSingle();
            Speed = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(ObjectType);
            writer.Write(StopTime);
            writer.Pad(2);
            writer.Write(Length);
            writer.Write(Speed);
        }
    }
}
