using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0302_RoadCap : SetObjectHeroes
    {
        public byte ObjectType { get; set; }
        public short ScaleX { get; set; }
        public short ScaleY { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            ObjectType = reader.ReadByte();
            reader.BaseStream.Position += 1;
            ScaleX = reader.ReadInt16();
            ScaleY = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(ObjectType);
            writer.Write((byte)0);
            writer.Write(ScaleX);
            writer.Write(ScaleY);
        }
    }
}