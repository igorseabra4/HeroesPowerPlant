using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1303_MovingCannon : SetObjectHeroes
    {
        public float MaxHeight { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            MaxHeight = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(MaxHeight);
        }
    }
}