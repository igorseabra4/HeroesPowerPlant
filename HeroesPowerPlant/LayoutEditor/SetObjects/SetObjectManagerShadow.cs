using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public abstract class SetObjectManagerShadow : SetObjectManager
    {
        public virtual void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            transformMatrix =
                Matrix.RotationX(MathUtil.DegreesToRadians(Rotation.X)) *
                Matrix.RotationY(MathUtil.DegreesToRadians(Rotation.Y)) *
                Matrix.RotationZ(MathUtil.DegreesToRadians(Rotation.Z)) *
                Matrix.Translation(Position);
        }
    }
}