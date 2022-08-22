using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0004_HintRing : SetObjectHeroes
    {
        public short LineToPlay { get; set; }
        public bool DeleteByLinkOff { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            LineToPlay = reader.ReadInt16();
            DeleteByLinkOff = reader.ReadByteBool();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(LineToPlay);
            writer.Write((byte)(DeleteByLinkOff ? 1 : 0));
        }
    }
}