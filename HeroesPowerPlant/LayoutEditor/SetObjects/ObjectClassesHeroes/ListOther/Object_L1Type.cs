using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_L1Type : SetObjectHeroes
    {
        public int ObjectType { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            ObjectType = reader.ReadInt32();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(ObjectType);
        }
    }
}