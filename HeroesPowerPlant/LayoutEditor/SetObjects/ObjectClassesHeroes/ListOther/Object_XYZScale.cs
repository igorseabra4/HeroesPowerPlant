using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_XYZScale : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix = Matrix.Scaling(ScaleX + 1f, ScaleY + 1f, ScaleZ + 1f)
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.Translation(Position);
        }

        public float ScaleX
        {
            get => ReadFloat(4);
            set { Write(4, value); CreateTransformMatrix(); }
        }

        public float ScaleY
        {
            get => ReadFloat(8);
            set { Write(8, value); CreateTransformMatrix(); }
        }

        public float ScaleZ
        {
            get => ReadFloat(12);
            set { Write(12, value); CreateTransformMatrix(); }
        }
    }
}