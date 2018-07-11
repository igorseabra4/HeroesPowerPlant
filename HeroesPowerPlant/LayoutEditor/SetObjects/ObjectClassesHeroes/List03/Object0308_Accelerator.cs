using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0308_Accelerator : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            return new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
        }

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix = Matrix.Scaling(ScaleX / 2, ScaleY / 2, ScaleZ / 2) *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z)) *
                Matrix.Translation(Position);
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            SharpRenderer.DrawCubeTrigger(transformMatrix, isSelected);
        }

        public float Speed
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float ScaleX
        {
            get { return ReadFloat(8); }
            set { Write(8, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float ScaleY
        {
            get { return ReadFloat(12); }
            set { Write(12, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float ScaleZ
        {
            get { return ReadFloat(16); }
            set { Write(16, value); CreateTransformMatrix(Position, Rotation); }
        }
    }
}