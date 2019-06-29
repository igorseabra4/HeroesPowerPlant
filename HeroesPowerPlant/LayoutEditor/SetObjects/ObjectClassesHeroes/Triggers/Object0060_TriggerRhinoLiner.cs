using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{

    public class Object0060_TriggerRhinoLiner : SetObjectManagerHeroes
    {
        private BoundingSphere sphereBound;

        public override bool TriangleIntersection(Ray r, string[][] modelNames, int miscSettingByte, float initialDistance, out float distance)
        {
            return r.Intersects(ref sphereBound, out distance);
        }

        public override BoundingBox CreateBoundingBox(string[][] modelNames, int miscSettingByte)
        {
            return new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
        }

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            sphereBound = new BoundingSphere(Position, Radius);
            transformMatrix = Matrix.Scaling(Radius * 2)
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.Translation(Position);
        }

        public override void Draw(SharpRenderer renderer, string[][] modelNames, int miscSettingByte, bool isSelected)
        {
            renderer.DrawSphereTrigger(transformMatrix, isSelected);
        }

        public byte Type
        {
            get => ReadByte(4);
            set => Write(4, value);
        }

        public byte SpeedControl
        {
            get => ReadByte(5);
            set => Write(5, value);
        }

        public byte NotInUse1
        {
            get => ReadByte(6);
            set => Write(6, value);
        }

        public byte NotInUse2
        {
            get => ReadByte(7);
            set => Write(7, value);
        }
        
        public float Radius
        {
            get => ReadFloat(8);
            set { Write(8, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float TargetX
        {
            get => ReadFloat(12);
            set { Write(12, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float TargetY
        {
            get => ReadFloat(16);
            set { Write(16, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float TargetZ
        {
            get => ReadFloat(20);
            set { Write(20, value); CreateTransformMatrix(Position, Rotation); }
        }
    }
}