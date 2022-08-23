using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object099A_Wanibreak : SetObjectHeroes
    {
        public byte ObjectType { get; set; }
        public byte Kazari { get; set; }
        public byte Unknown1 { get; set; }
        public byte Unknown2 { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            ObjectType = reader.ReadByte();
            Kazari = reader.ReadByte();
            Unknown1 = reader.ReadByte();
            Unknown2 = reader.ReadByte();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(ObjectType);
            writer.Write(Kazari);
            writer.Write(Unknown1);
            writer.Write(Unknown2);
        }
    }
}