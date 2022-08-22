using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object04_CraneWallLight : SetObjectHeroes
    {
        public short RotSpeed { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            RotSpeed = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(RotSpeed);
        }
    }
}
