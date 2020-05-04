using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum StartMode
    {
        Lit = 0,
        LitOnRange = 1,
        Unlit = 2
    }

    public class Object1104_FlameTorch : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = (IsUpsideDown ? Matrix.RotationY(MathUtil.Pi) : Matrix.Identity) *
                Matrix.Scaling(Scale + 1f) * DefaultTransformMatrix();

            CreateBoundingBox();
        }

        private string FloorBase = "S11_ON_FIREA_BASE.DFF";
        private string FloorBlue = "S11_ON_FIREA_BLUE.DFF";
        private string FloorRed = "S11_ON_FIREA_RED.DFF";
        private string AirBase = "S11_ON_FIREB_BASE.DFF";
        private string AirBlue = "S11_ON_FIREB_BLUE.DFF";
        private string AirRed = "S11_ON_FIREB_RED.DFF";

        public override void Draw(SharpRenderer renderer)
        {
            if (BaseType == BaseTypeEnum.None)
                DrawCube(renderer);
            else if (BaseType == BaseTypeEnum.Floor)
            {
                if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(FloorBase))
                    Draw(renderer, FloorBase);
                if (IsBlue)
                {
                    if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(FloorBase))
                        Draw(renderer, FloorBlue);
                }
                else
                {
                    if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(FloorRed))
                        Draw(renderer, FloorRed);
                }
            }
            else if (BaseType == BaseTypeEnum.Air)
            {
                if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(AirBase))
                    Draw(renderer, AirBase);
                if (IsBlue)
                {
                    if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(AirBase))
                        Draw(renderer, AirBlue);
                }
                else
                {
                    if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(AirRed))
                        Draw(renderer, AirRed);
                }
            }
        }

        public bool IsBlue
        {
            get => ReadInt(4) != 0;
            set => Write(4, value ? 1 : 0);
        }

        public StartMode StartMode
        {
            get => (StartMode)ReadInt(8);
            set => Write(8, (int)value);
        }

        public float Range
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float Scale
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public bool IsUpsideDown
        {
            get => ReadByte(20) != 0;
            set => Write(20, (byte)(value ? 1 : 0));
        }

        public enum BaseTypeEnum
        {
            None = 0,
            Floor = 1,
            Air = 2
        }
        public BaseTypeEnum BaseType
        {
            get => (BaseTypeEnum)ReadByte(21);
            set => Write(21, (byte)value);
        }

        public bool HasSE
        {
            get => ReadByte(22) != 0;
            set => Write(22, (byte)(value ? 1 : 0));
        }

        public bool HasCollision
        {
            get => ReadByte(23) != 0;
            set => Write(23, (byte)(value ? 1 : 0));
        }
    }
}
