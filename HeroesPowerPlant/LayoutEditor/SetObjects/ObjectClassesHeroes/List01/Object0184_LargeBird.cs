using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0184_LargeBird : SetObjectHeroes
    {
        public float Radius { get; set; }
        public float Speed { get; set; }
        public float Scale { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Radius = reader.ReadSingle();
            Speed = reader.ReadSingle();
            Scale = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Radius);
            writer.Write(Speed);
            writer.Write(Scale);
        }
    }
}
