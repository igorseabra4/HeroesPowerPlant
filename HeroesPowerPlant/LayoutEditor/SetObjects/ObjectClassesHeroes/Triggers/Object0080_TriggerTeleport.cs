using HeroesPowerPlant.Shared.Utilities;
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

        public float Radius { get; set; }
        public float DestinationX { get; set; }
        public float DestinationY { get; set; }
        public float DestinationZ { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Radius = reader.ReadSingle();
            DestinationX = reader.ReadSingle();
            DestinationY = reader.ReadSingle();
            DestinationZ = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Radius);
            writer.Write(DestinationX);
            writer.Write(DestinationY);
            writer.Write(DestinationZ);
        }
    }
}