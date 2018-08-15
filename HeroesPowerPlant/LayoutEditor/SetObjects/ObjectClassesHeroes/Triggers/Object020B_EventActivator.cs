using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

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

        public override void Draw(string[] modelNames, bool isSelected)
        {
            DrawCubeTrigger(transformMatrix, isSelected);
        }

        public float ScaleX
        {
            get { return ReadFloat(4); }
            set { Write(4, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float ScaleY
        {
            get { return ReadFloat(8); }
            set { Write(8, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float ScaleZ
        {
            get { return ReadFloat(12); }
            set { Write(12, value); CreateTransformMatrix(Position, Rotation); }
        }

        public EventActivatorType Type
        {
            get { return (EventActivatorType)ReadByte(16); }
            set { Write(16, (byte)value); }
        }

        public bool OnlyLeader
        {
            get { return ReadByte(17) != 0; }
            set { Write(17, value ? (byte)1 : (byte)0); }
        }
    }
}
