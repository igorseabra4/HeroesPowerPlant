using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0080_TriggerTeleport : SetObjectManagerHeroes
    {
        private BoundingSphere sphereBound;

        public override bool TriangleIntersection(Ray r, string[] ModelNames)
        {
            return r.Intersects(sphereBound);
        }

        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            return new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
        }

        private Matrix destinationMatrix;

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            sphereBound = new BoundingSphere(Position, Radius );

            destinationMatrix = Matrix.Translation(XDestination * 2, YDestination * 2, ZDestination * 2);

            transformMatrix = Matrix.Scaling(Radius)
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.Translation(Position);
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            DrawSphereTrigger(transformMatrix, isSelected);

            if (isSelected)
                DrawCube(true);
        }

        public float Radius
        {
            get { return ReadFloat(4); }
            set { Write(4, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float XDestination
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float YDestination
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public float ZDestination
        {
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }
    }
}