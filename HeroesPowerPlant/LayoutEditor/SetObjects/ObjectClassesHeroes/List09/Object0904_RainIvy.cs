using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0904_RainIvy : SetObjectHeroes
    {
        public float Range { get; set; }
        public float MotionSpeed { get; set; }
        public int NotInUse { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Range = reader.ReadSingle();
            MotionSpeed = reader.ReadSingle();
            NotInUse = reader.ReadInt32();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Range);
            writer.Write(MotionSpeed);
            writer.Write(NotInUse);
        }
    }
}