using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object090B_IvyJump : SetObjectHeroes
    {
        public float Target1X { get; set; }
        public float Target1Y { get; set; }
        public float Target1Z { get; set; }
        public float Target2X { get; set; }
        public float Target2Y { get; set; }
        public float Target2Z { get; set; }
        public short KazariType { get; set; }
        public short Dammy { get; set; }
        public short NoControlTime { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Target1X = reader.ReadSingle();
            Target1Y = reader.ReadSingle();
            Target1Z = reader.ReadSingle();
            Target2X = reader.ReadSingle();
            Target2Y = reader.ReadSingle();
            Target2Z = reader.ReadSingle();
            KazariType = reader.ReadInt16();
            Dammy = reader.ReadInt16();
            NoControlTime = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Target1X);
            writer.Write(Target1Y);
            writer.Write(Target1Z);
            writer.Write(Target2X);
            writer.Write(Target2Y);
            writer.Write(Target2Z);
            writer.Write(KazariType);
            writer.Write(Dammy);
            writer.Write(NoControlTime);
        }
    }
}