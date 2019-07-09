using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object2588_Decoration1 : SetObjectManagerShadow
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ)
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.Translation(Position);
        }

        public int Type
        {
            get => ReadInt(0x0);
            set => Write(0x0, value);
        }

        public float ScaleX
        {
            get => ReadFloat(0x4);
            set { Write(0x4, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float ScaleY
        {
            get => ReadFloat(0x8);
            set { Write(0x8, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float ScaleZ
        {
            get => ReadFloat(0xC);
            set { Write(0xC, value); CreateTransformMatrix(Position, Rotation); }
        }
    }
}
