using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0001_Spring : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = DefaultTransformMatrix(MathUtil.Pi);
            CreateBoundingBox();
        }

        public float Power
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public short NoControlTime
        {
            get => ReadShort(8);
            set => Write(8, value);
        }

        public float GuideLine
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}
