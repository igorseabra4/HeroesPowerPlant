using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum FlowerType : byte
    {
        Item = 0,
        Scaffold = 1,
        Warp = 2
    }

    public class Object0032_WarpFlower : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        public FlowerType FlowerType
        {
            get => (FlowerType)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public float Scale
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float RisingHeight
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}