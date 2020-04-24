using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum GhostType
    {
        NoMove = 0,
        Line = 1,
        Circle = 2
    }

    public class Object1105_Ghost : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            base.CreateTransformMatrix();
            transformMatrix = Matrix.Scaling(Scale + 1f) * transformMatrix;

            CreateBoundingBox();
        }

        public GhostType GhostType
        {
            get => (GhostType)ReadInt(4);
            set => Write(4, (int)value);
        }

        public float Range
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float MovingArea
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float Speed
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float Scale
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }
    }
}
