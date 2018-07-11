using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object020B_EventActivator : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            return new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
        }

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
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
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float ScaleY
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float ScaleZ
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public enum TypeType
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
        public TypeType Type
        {
            get { return (TypeType)ReadWriteByte(16); }
            set { ReadWriteSingle(16, (byte)value); }
        }

        public bool OnlyLeader
        {
            get { return ReadWriteByte(17) != 0; }
            set { ReadWriteByte(17, value ? (byte)1 : (byte)0); }
        }
    }
}
