using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0012_ItemCapsule : SetObjectShadow
    {
        public EShadowItem Item { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            Item = (EShadowItem)reader.ReadInt32();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)Item);
        }
    }
}
