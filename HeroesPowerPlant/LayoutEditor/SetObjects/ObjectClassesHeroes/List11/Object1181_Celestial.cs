using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1181_Celestial : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix = Matrix.Scaling(Scale != 0f ? Scale : 1f) *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.Translation(Position);
        }

        public int Type
        {
            get { return ReadLong(4); }
            set { Write(4, value); }
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

        public float Scale
        {
            get { return ReadFloat(20); }
            set { Write(20, value); CreateTransformMatrix(Position, Rotation); }
        }
    }
}