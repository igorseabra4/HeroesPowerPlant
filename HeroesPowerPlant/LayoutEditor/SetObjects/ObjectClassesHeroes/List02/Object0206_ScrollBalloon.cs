using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0206_ScrollBalloon : SetObjectHeroes
    {
        public EHeroesItem Item { get; set; }
        public float Scale { get; set; }
        public float Speed { get; set; }
        public float EndX { get; set; }
        public float EndY { get; set; }
        public float EndZ { get; set; }
        public short InvokeOffX { get; set; }
        public short InvokeOffY { get; set; }
        public short InvokeOffZ { get; set; }
        public short InvokeSize { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Item = (EHeroesItem)reader.ReadByte();
            reader.BaseStream.Position += 3;
            Scale = reader.ReadSingle();
            Speed = reader.ReadSingle();
            EndX = reader.ReadSingle();
            EndY = reader.ReadSingle();
            EndZ = reader.ReadSingle();
            InvokeOffX = reader.ReadInt16();
            InvokeOffY = reader.ReadInt16();
            InvokeOffZ = reader.ReadInt16();
            InvokeSize = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)Item);
            writer.Pad(3);
            writer.Write(Scale);
            writer.Write(Speed);
            writer.Write(EndX);
            writer.Write(EndY);
            writer.Write(EndZ);
            writer.Write(InvokeOffX);
            writer.Write(InvokeOffY);
            writer.Write(InvokeOffZ);
            writer.Write(InvokeSize);
        }
    }
}
