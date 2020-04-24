using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000B_DashPanel : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix =
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y)) *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.Translation(Position);

            CreateBoundingBox();
        }

        public float Speed
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public short NoControlTime
        {
            get => ReadShort(8);
            set => Write(8, value);
        }
    }
}