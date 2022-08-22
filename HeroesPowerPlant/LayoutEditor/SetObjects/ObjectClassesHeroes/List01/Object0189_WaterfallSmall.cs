using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0189_WaterfallSmall : SetObjectHeroes
    {
        public byte ObjectType { get; set; }
        public float Scale { get; set; }
        public float Speed { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            ObjectType = reader.ReadByte();
            reader.BaseStream.Position += 3;
            Scale = reader.ReadSingle();
            Speed = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(ObjectType);
            writer.Pad(3);
            writer.Write(Scale);
            writer.Write(Speed);
        }
    }
}
