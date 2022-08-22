using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0908_RainFruit : SetObjectHeroes
    {
        public float Range { get; set; }
        public float Power { get; set; }
        public short NoControlTime { get; set; }
        public short FruitType { get; set; }
        public short DeleteTimeSec { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Range = reader.ReadSingle();
            Power = reader.ReadSingle();
            NoControlTime = reader.ReadInt16();
            FruitType = reader.ReadInt16();
            DeleteTimeSec = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Range);
            writer.Write(Power);
            writer.Write(NoControlTime);
            writer.Write(FruitType);
            writer.Write(DeleteTimeSec);
        }
    }
}