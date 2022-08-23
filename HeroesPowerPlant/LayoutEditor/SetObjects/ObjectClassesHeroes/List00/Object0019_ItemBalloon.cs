using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0019_ItemBalloon : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        public EHeroesItem Item { get; set; }
        public float Scale { get; set; }
        [Description("Usually 0")]
        public short Unknown { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Item = (EHeroesItem)reader.ReadByte();
            reader.BaseStream.Position += 3;
            Scale = reader.ReadSingle();
            reader.BaseStream.Position += 4;
            Unknown = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)Item);
            writer.Pad(3);
            writer.Write(Scale);
            writer.Pad(4);
            writer.Write(Unknown);
        }
    }
}