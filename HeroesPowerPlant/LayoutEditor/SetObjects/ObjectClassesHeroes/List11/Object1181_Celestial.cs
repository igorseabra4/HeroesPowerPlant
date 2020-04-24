using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1181_Celestial : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            base.CreateTransformMatrix();
            transformMatrix = Matrix.Scaling(Scale + 1f) * transformMatrix;

            CreateBoundingBox();
        }

        public int CelestialType
        {
            get => ReadInt(4);
            set => Write(4, value);
        }

        public float SpeedX
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float SpeedY
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float SpeedZ
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