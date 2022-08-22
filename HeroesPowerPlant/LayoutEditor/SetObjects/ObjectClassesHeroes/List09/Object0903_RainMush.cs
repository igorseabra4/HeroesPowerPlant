using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0903_RainMush : SetObjectHeroes
    {
        public float Range { get; set; }
        public float Power { get; set; }
        public byte NoControlTime { get; set; }
        public byte ObjectType { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Range = reader.ReadSingle();
            Power = reader.ReadSingle();
            NoControlTime = reader.ReadByte();
            ObjectType = reader.ReadByte();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Range);
            writer.Write(Power);
            writer.Write(NoControlTime);
            writer.Write(ObjectType);
        }
    }
}