using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object020A_ColliQuake : SetObjectHeroes
    {
        public float ScaleX { get; set; }
        public float ScaleY { get; set; }
        public float ScaleZ { get; set; }
        public float Strength { get; set; }
        public int Time { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            ScaleX = reader.ReadSingle();
            ScaleY = reader.ReadSingle();
            ScaleZ = reader.ReadSingle();
            Strength = reader.ReadSingle();
            Time = reader.ReadInt32();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(ScaleX);
            writer.Write(ScaleY);
            writer.Write(ScaleZ);
            writer.Write(Strength);
            writer.Write(Time);
        }
    }
}
