using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000C_DashRing : Object000B_DashPanel
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix =
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y) + MathUtil.Pi) *
                Matrix.Translation(Position);

            CreateBoundingBox();
        }
    }
}