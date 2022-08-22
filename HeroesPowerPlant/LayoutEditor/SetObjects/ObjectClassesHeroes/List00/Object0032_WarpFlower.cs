using HeroesPowerPlant.Shared.Utilities;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0032_WarpFlower : SetObjectHeroes
    {
        public enum EFlowerType : byte
        {
            Item = 0,
            Scaffold = 1,
            Warp = 2
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        public EFlowerType FlowerType { get; set; }
        public float Scale { get; set; }
        public float RisingHeight { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            FlowerType = (EFlowerType)reader.ReadByte();
            reader.BaseStream.Position += 3;
            Scale = reader.ReadSingle();
            RisingHeight = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)FlowerType);
            writer.Pad(3);
            writer.Write(Scale);
            writer.Write(RisingHeight);
        }
    }
}