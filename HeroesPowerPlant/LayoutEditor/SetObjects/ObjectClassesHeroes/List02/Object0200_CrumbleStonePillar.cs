using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{

    public class Object0200_CrumbleStonePillar : SetObjectHeroes
    {
        public enum EPillarType : byte
        {
            Left = 0,
            Right = 1,
            Center = 2
        }

        public EPillarType PillarType { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            PillarType = (EPillarType)reader.ReadByte();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)PillarType);
        }
    }
}
