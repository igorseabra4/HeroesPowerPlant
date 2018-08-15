using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000C_DashRing : Object000B_DashPanel
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix =
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y) + MathUtil.Pi) *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.Translation(Position);
        }
    }
}