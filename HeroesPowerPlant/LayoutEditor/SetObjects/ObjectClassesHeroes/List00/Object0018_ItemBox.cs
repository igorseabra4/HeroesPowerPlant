using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0018_ItemBox : SetObjectHeroes
    {
        public EHeroesItem Item { get; set; }
        public bool HomingOff { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Item = (EHeroesItem)reader.ReadByte();
            HomingOff = reader.ReadByteBool();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)Item);
            writer.Write((byte)(HomingOff ? 1 : 0));
        }
    }
}