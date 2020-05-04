using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0080_TriggerTeleport : SetObjectHeroes
    {
        private BoundingSphere sphereBound;
        private Matrix destinationMatrix;

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Radius * 2) * DefaultTransformMatrix();

            sphereBound = new BoundingSphere(Position, Radius * 2);
            boundingBox = BoundingBox.FromSphere(sphereBound);

            destinationMatrix = Matrix.Scaling(5) * Matrix.Translation(XDestination, YDestination, ZDestination);
        }
        
        public override void Draw(SharpRenderer renderer)
        {
            renderer.DrawSphereTrigger(transformMatrix, isSelected);

            if (isSelected)
                renderer.DrawCubeTrigger(destinationMatrix, isSelected);
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            return r.Intersects(ref sphereBound, out distance);
        }
        
        public float Radius
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float XDestination
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float YDestination
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float ZDestination
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }
    }
}