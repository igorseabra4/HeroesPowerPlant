using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0025_FormSign : SetObjectHeroes
    {
        public EFormation Formation { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Formation = (EFormation)reader.ReadByte();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)Formation);
        }
    }
}