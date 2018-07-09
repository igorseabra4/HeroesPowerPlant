using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_F1Scale : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            transformMatrix = Matrix.Scaling(Scale == 0f ? 1f : Scale)
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.Translation(Position);
        }

        public float Scale
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }
    }
}