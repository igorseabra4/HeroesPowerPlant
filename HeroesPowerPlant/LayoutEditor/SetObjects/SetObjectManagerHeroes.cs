using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public abstract class SetObjectManagerHeroes : SetObjectManager
    {
        public virtual void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            transformMatrix =
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.Translation(Position);
        }
    }
}