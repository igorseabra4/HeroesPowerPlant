using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object2588_Decoration1 : SetObjectShadow
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        public int DecorationType
        {
            get => ReadInt(0x0);
            set => Write(0x0, value);
        }

        public float ScaleX
        {
            get => ReadFloat(0x4);
            set => Write(0x4, value);
        }

        public float ScaleY
        {
            get => ReadFloat(0x8);
            set => Write(0x8, value);
        }

        public float ScaleZ
        {
            get => ReadFloat(0xC);
            set => Write(0xC, value);
        }
    }
}
