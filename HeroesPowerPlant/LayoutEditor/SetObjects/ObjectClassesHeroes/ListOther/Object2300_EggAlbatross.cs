using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object2300_EggAlbatross : SetObjectHeroes
    {
        public float YOffset { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            YOffset = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(YOffset);
        }
    }
}