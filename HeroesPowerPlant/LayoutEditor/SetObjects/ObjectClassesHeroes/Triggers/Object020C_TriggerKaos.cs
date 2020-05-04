using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object020C_TriggerKaos : SetObjectHeroes
    {
        private BoundingSphere sphereBound;

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale) * DefaultTransformMatrix();
            sphereBound = new BoundingSphere(Position, Scale);
            boundingBox = BoundingBox.FromSphere(sphereBound);
        }

        public override void Draw(SharpRenderer renderer)
        {
            renderer.DrawSphereTrigger(transformMatrix, isSelected);
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            return r.Intersects(ref sphereBound, out distance);
        }

        public float Scale
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public byte KaosType
        {
            get => ReadByte(8);
            set => Write(8, value);
        }

        public byte Param2
        {
            get => ReadByte(9);
            set => Write(9, value);
        }

        public float Param3
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}
