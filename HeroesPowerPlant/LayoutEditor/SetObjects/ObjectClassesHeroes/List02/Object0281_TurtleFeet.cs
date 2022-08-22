using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0281_TurtleFeet : SetObjectHeroes
    {
        public float Scale { get; set; }
        public float Speed { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Scale = reader.ReadSingle();
            Speed = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Scale);
            writer.Write(Speed);
        }
    }
}
