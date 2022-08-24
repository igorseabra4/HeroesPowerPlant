using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0080_TriggerTeleport : SetObjectHeroes
    {
        public override bool IsTrigger() => true;

        private BoundingSphere sphereBound;
        private Matrix destinationMatrix;

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Radius * 2) * DefaultTransformMatrix();

            sphereBound = new BoundingSphere(Position, Radius * 2);
            boundingBox = BoundingBox.FromSphere(sphereBound);

            destinationMatrix = Matrix.Scaling(5) * Matrix.Translation(DestinationX, DestinationY, DestinationZ);
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

        [MiscSetting]
        public float Radius { get; set; }
        [MiscSetting]
        public float DestinationX { get; set; }
        [MiscSetting]
        public float DestinationY { get; set; }
        [MiscSetting]
        public float DestinationZ { get; set; }
    }
}