using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000B_DashPanel : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix =
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y)) *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.Translation(Position);
        }

        public float Speed
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public short ControlTime
        {
            get => ReadShort(8);
            set => Write(8, value);
        }
    }
}