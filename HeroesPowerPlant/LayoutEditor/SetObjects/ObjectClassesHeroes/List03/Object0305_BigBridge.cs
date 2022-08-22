using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0305_BigBridge : SetObjectHeroes
    {
        public float ColScaleX { get; set; }
        public float ColScaleY { get; set; }
        public float ColScaleZ { get; set; }
        public float AnimSpeed { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            ColScaleX = reader.ReadSingle();
            ColScaleY = reader.ReadSingle();
            ColScaleZ = reader.ReadSingle();
            AnimSpeed = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(ColScaleX);
            writer.Write(ColScaleY);
            writer.Write(ColScaleZ);
            writer.Write(AnimSpeed);
        }
    }
}