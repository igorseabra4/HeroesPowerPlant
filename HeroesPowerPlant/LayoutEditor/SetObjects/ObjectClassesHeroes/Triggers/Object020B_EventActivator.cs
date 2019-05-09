using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum EventActivatorType : byte
    {
        NotInUse = 0,
        NotInUse2 = 1,
        Elevator0402 = 2,
        EnergyUp0412 = 3,
        Shutter0410 = 4,
        BallGLSOn0480 = 5,
        BallGLSOff0480 = 6,
        SenkanMov = 7,
        Hakai1320 = 8,
        FallAshiba1400 = 9
    }

    public class Object020B_EventActivator : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            return new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
        }

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

        public override void Draw(SharpRenderer renderer, string[] modelNames, bool isSelected)
        {
            renderer.DrawCubeTrigger(transformMatrix, isSelected);
        }

        public float ScaleX
        {
            get => ReadFloat(4);
            set { Write(4, value); CreateTransformMatrix(); }
        }

        public float ScaleY
        {
            get => ReadFloat(8);
            set { Write(8, value); CreateTransformMatrix(); }
        }

        public float ScaleZ
        {
            get => ReadFloat(12);
            set { Write(12, value); CreateTransformMatrix(); }
        }

        public EventActivatorType Type
        {
            get => (EventActivatorType)ReadByte(16);
            set => Write(16, (byte)value);
        }

        public bool OnlyLeader
        {
            get => ReadByte(17) != 0;
            set => Write(17, value ? (byte)1 : (byte)0);
        }
    }
}
