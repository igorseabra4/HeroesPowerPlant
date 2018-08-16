using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object01FF_SetParticle : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            return new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
        }

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            Vector3 box = Program.ParticleEditor.GetBoxForSetParticle(Number - 50);

            if (box != Vector3.Zero)
            {
                this.Position = Position;
                this.Rotation = Rotation;

                transformMatrix = Matrix.Scaling(box * 2) *
                    Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                    Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y)) *
                    Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                    Matrix.Translation(Position);
            }
            else base.CreateTransformMatrix(Position, Rotation);
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            DrawCubeTrigger(transformMatrix, isSelected);
        }

        public byte Number
        {
            get { return ReadByte(4); }
            set { Write(4, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float SpeedX
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float SpeedY
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public float SpeedZ
        {
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }

        public float UnknownFloat
        {
            get { return ReadFloat(20); }
            set { Write(20, value); }
        }

        public int UnknownInteger
        {
            get { return ReadLong(24); }
            set { Write(24, value); }
        }

        public byte UnknownByte1
        {
            get { return ReadByte(28); }
            set { Write(28, value); }
        }

        public byte UnknownByte2
        {
            get { return ReadByte(29); }
            set { Write(29, value); }
        }

        public byte UnknownByte3
        {
            get { return ReadByte(30); }
            set { Write(30, value); }
        }
    }
}
