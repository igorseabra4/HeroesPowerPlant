using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_IntTypeFloatScale : SetObjectManagerHeroes
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

        public int Type
        {
            get => ReadInt(4);
            set => Write(4, value);
        }

        public float Scale
        {
            get => ReadFloat(8);
            set { Write(8, value); CreateTransformMatrix(); }
        }
    }
}