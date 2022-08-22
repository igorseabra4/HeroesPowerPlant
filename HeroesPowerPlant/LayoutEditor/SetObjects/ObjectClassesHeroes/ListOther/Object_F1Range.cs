using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_F1Range : SetObjectHeroes
    {
        public float Range { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Range = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Range);
        }
    }
}