using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1304_RectFloatingPlatform : SetObjectHeroes
    {
        public float Speed { get; set; }
        public float EndPosX { get; set; }
        public float EndPosY { get; set; }
        public float EndPosZ { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Speed = reader.ReadSingle();
            EndPosX = reader.ReadSingle();
            EndPosY = reader.ReadSingle();
            EndPosZ = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Speed);
            writer.Write(EndPosX);
            writer.Write(EndPosY);
            writer.Write(EndPosZ);
        }
    }
}