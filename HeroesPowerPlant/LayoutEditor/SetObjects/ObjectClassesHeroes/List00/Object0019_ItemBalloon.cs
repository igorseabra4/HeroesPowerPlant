using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0019_ItemBalloon : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix = Matrix.Scaling(Scale + 1f) *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.Translation(Position);
        }

        public Item Item
        {
            get { return (Item)ReadByte(4); }
            set { Write(4, (byte)value); }
        }

        public float Scale
        {
            get { return ReadFloat(8); }
            set { Write(8, value); CreateTransformMatrix(Position, Rotation); }
        }
    }
}