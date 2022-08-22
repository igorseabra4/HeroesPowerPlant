using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object090E_Alligator : SetObjectHeroes
    {
        public float StartPosX { get; set; }
        public float StartPosY { get; set; }
        public float StartPosZ { get; set; }
        public float StartRange { get; set; }
        public float SpeedRate { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            StartPosX = reader.ReadSingle();
            StartPosY = reader.ReadSingle();
            StartPosZ = reader.ReadSingle();
            StartRange = reader.ReadSingle();
            SpeedRate = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(StartPosX);
            writer.Write(StartPosY);
            writer.Write(StartPosZ);
            writer.Write(StartRange);
            writer.Write(SpeedRate);
        }
    }
}