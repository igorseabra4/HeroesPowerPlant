using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object258A_Effect1 : SetObjectShadow
    {

        public int EffectType
        {
            get => ReadInt(0);
            set => Write(0, value);
        }

        public float ScaleX
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float ScaleY
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float ScaleZ
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ);
            transformMatrix *= DefaultTransformMatrix();
            CreateBoundingBox();
        }
    }
}

