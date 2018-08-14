using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1104_FlameTorch : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix = Matrix.Scaling(Scale == 0f ? 1f : Scale + 1f) *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.Translation(Position);
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (BaseType == BaseTypeEnum.None)
                DrawCube(isSelected);
            else if (BaseType == BaseTypeEnum.Floor)
            {
                Draw("S11_ON_FIREA_BASE.DFF", isSelected);
                if (IsBlue)
                    Draw("S11_ON_FIREA_BLUE.DFF", isSelected);
                else
                    Draw("S11_ON_FIREA_RED.DFF", isSelected);
            }
            else if (BaseType == BaseTypeEnum.Air)
            {
                Draw("S11_ON_FIREB_BASE.DFF", isSelected);
                if (IsBlue)
                    Draw("S11_ON_FIREB_BLUE.DFF", isSelected);
                else
                    Draw("S11_ON_FIREB_RED.DFF", isSelected);
            }
        }

        public bool IsBlue
        {
            get { return ReadLong(4) != 0; }
            set { Write(4, value ? 1 : 0); }
        }
        
        public enum StartModeEnum
        {
            Lit = 0,
            LitOnRange = 1,
            Unlit = 2
        }
        public StartModeEnum StartMode
        {
            get { return (StartModeEnum)ReadLong(8); }
            set { Write(8, (int)value); }
        }

        public float Range
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public float Scale
        {
            get { return ReadFloat(16); }
            set { Write(16, value); CreateTransformMatrix(Position, Rotation); }
        }

        public bool IsUpsideDown
        {
            get { return ReadByte(20) != 0; }
            set { Write(20, (byte)(value ? 1 : 0)); }
        }

        public enum BaseTypeEnum
        {
            None = 0,
            Floor = 1,
            Air = 2
        }
        public BaseTypeEnum BaseType
        {
            get { return (BaseTypeEnum)ReadByte(21); }
            set { byte a = (byte)value; Write(21, a); }
        }

        public bool HasSE
        {
            get { return ReadByte(22) != 0; }
            set { Write(22, (byte)(value ? 1 : 0)); }
        }

        public bool HasCollision
        {
            get { return ReadByte(23) != 0; }
            set { Write(23, (byte)(value ? 1 : 0)); }
        }
    }
}
