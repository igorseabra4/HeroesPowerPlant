using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1102_CastleWall : SetObjectHeroes
    {
        public int ObjectType { get; set; }
        public bool IsUpsideDown { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            ObjectType = reader.ReadInt32();
            IsUpsideDown = reader.ReadInt32Bool();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(ObjectType);
            writer.Write((byte)(IsUpsideDown ? 1 : 0));
        }
    }
}
