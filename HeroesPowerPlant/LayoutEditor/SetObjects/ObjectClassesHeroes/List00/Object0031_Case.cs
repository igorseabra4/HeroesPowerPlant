using HeroesPowerPlant.Shared.Utilities;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0031_Case : SetObjectHeroes
    {
        public enum EDirection : byte
        {
            Up = 0,
            Down = 1,
        }

        public float ScaleX { get; set; }
        public float ScaleY { get; set; }
        public float ScaleZ { get; set; }
        [Description("Doesn't use actual Link ID. Use this one.")]
        public byte LinkID { get; set; }
        public EDirection Direction { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            ScaleX = reader.ReadSingle();
            ScaleY = reader.ReadSingle();
            ScaleZ = reader.ReadSingle();
            LinkID = reader.ReadByte();
            Direction = (EDirection)reader.ReadByte();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(ScaleX);
            writer.Write(ScaleY);
            writer.Write(ScaleZ);
            writer.Write(LinkID);
            writer.Write((byte)Direction);
        }
    }
}