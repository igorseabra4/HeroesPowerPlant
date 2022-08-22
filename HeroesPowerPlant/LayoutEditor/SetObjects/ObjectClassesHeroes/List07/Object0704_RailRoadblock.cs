using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0704_RailRoadblock : SetObjectHeroes
    {
        public float Range { get; set; }
        public int Speed { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Range = reader.ReadSingle();
            Speed = reader.ReadInt32();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Range);
            writer.Write(Speed);
        }
    }
}