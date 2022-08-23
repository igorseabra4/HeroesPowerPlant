using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object04_CraneWallLight : SetObjectHeroes
    {
        public int RotSpeed { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            RotSpeed = reader.ReadInt32();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(RotSpeed);
        }
    }
}
