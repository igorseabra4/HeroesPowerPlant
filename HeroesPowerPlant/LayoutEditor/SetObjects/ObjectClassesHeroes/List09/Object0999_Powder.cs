using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0999_Powder : SetObjectHeroes
    {
        public float Scale { get; set; }
        public int ObjectType { get; set; }
        public int Number { get; set; }
        public int Time { get; set; }
        public float RangeR { get; set; }
        public float RangeY { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Scale = reader.ReadSingle();
            ObjectType = reader.ReadInt32();
            Number = reader.ReadInt32();
            Time = reader.ReadInt32();
            RangeR = reader.ReadSingle();
            RangeY = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Scale);
            writer.Write(ObjectType);
            writer.Write(Number);
            writer.Write(Time);
            writer.Write(RangeR);
            writer.Write(RangeY);
        }
    }
}