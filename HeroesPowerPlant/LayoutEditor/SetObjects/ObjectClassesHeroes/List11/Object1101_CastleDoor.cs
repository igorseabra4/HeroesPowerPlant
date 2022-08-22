using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1101_CastleDoor : SetObjectHeroes
    {
        public bool IsUpsideDown { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            IsUpsideDown = reader.ReadInt32Bool();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)(IsUpsideDown ? 1 : 0));
        }
    }
}
