using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0908_RainFruit : SetObjectHeroes
    {
        public float Range { get; set; }
        public float Power { get; set; }
        public short NoControlTime { get; set; }
        public short ObjectType { get; set; }
        public int UnknownInt { get; set; }
        public float UnknownFloat1 { get; set; }
        public float UnknownFloat2 { get; set; }
        public short UnknownShort1 { get; set; }
        public short UnknownShort2 { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Range = reader.ReadSingle();
            Power = reader.ReadSingle();
            NoControlTime = reader.ReadInt16();
            ObjectType = reader.ReadInt16();
            UnknownInt = reader.ReadInt32();
            UnknownFloat1 = reader.ReadSingle();
            UnknownFloat2 = reader.ReadSingle();
            UnknownShort1 = reader.ReadInt16();
            UnknownShort2 = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Range);
            writer.Write(Power);
            writer.Write(NoControlTime);
            writer.Write(ObjectType);
            writer.Write(UnknownInt);
            writer.Write(UnknownFloat1);
            writer.Write(UnknownFloat2);
            writer.Write(UnknownShort1);
            writer.Write(UnknownShort2);
        }
    }
}