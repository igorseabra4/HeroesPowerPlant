using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum SpikeBallType
    {
        SingleBall = 0,
        DoubleBall = 1
    }

    public class Object0015_SpikeBall : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        public SpikeBallType SpikeBallType
        {
            get => (SpikeBallType)ReadInt(4);
            set => Write(4, (int)value);
        }

        public float RotateSpeed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Scale
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}