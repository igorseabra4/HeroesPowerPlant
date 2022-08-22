using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0007_Target : SetObjectHeroes
    {
        public EHeroesItem Item { get; set; }
        public byte AppearMode { get; set; }
        public byte LinkID { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Item = (EHeroesItem)reader.ReadByte();
            AppearMode = reader.ReadByte();
            LinkID = reader.ReadByte();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)Item);
            writer.Write(AppearMode);
            writer.Write(LinkID);
        }
    }
}