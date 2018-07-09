using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_XYZScale : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            transformMatrix = Matrix.Scaling(ScaleX == 0 ? 1f : ScaleX, ScaleY == 0 ? 1f : ScaleY, ScaleZ == 0 ? 1f : ScaleZ)
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.Translation(Position);
        }

        public float ScaleX
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float ScaleY
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float ScaleZ
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }
    }
}