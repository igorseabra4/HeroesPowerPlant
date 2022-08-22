using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0380_BalloonDesign : SetObjectHeroes
    {
        public byte BalloonType { get; set; }
        public int SpdRad { get; set; }
        public float Radius { get; set; }
        public float Scale { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            BalloonType = reader.ReadByte();
            reader.BaseStream.Position += 3;
            SpdRad = reader.ReadInt32();
            Radius = reader.ReadSingle();
            Scale = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(BalloonType);
            writer.Pad(3);
            writer.Write(SpdRad);
            writer.Write(Radius);
            writer.Write(Scale);
        }
    }
}