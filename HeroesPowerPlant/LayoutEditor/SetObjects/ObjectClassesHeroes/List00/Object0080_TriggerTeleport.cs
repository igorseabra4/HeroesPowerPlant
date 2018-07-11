using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0080_TriggerTeleport : SetObjectManagerHeroes
    {
        public override bool TriangleIntersection(Ray r, string[] ModelNames)
        {
            Vector3 center = Vector3.Zero;
            center = (Vector3)Vector3.Transform(center, transformMatrix);

            return r.Intersects(new BoundingSphere(center, Radius / 2));
        }

        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            return new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
        }

        private Matrix destinationMatrix;

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            destinationMatrix = Matrix.Translation(XDestination, YDestination, ZDestination);

            transformMatrix = Matrix.Scaling(Radius)
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.Translation(Position);
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            DrawCube(isSelected);
            DrawSphereTrigger(transformMatrix, isSelected);
        }

        public float Radius
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float XDestination
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float YDestination
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public float ZDestination
        {
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }
    }
}