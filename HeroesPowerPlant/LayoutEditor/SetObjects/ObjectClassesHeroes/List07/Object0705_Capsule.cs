using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0705_Capsule : SetObjectHeroes
    {
        public EHeroesItem Item { get; set; }
        public byte ObjectType { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Item = (EHeroesItem)reader.ReadByte();
            ObjectType = reader.ReadByte();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)Item);
            writer.Write(ObjectType);
        }
    }
}