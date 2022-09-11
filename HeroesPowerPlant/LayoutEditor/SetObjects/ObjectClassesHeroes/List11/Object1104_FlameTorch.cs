using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1104_FlameTorch : SetObjectHeroes
    {
        public enum EStartMode : int
        {
            Lit = 0,
            LitOnRange = 1,
            Unlit = 2
        }

        public enum EBaseType : byte
        {
            None = 0,
            Floor = 1,
            Air = 2
        }

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
            if (BaseType == EBaseType.None)
                DrawCube(renderer);
            else if (BaseType == EBaseType.Floor)
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
            else if (BaseType == EBaseType.Air)
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

        protected void Draw(SharpRenderer renderer, string modelName)
        {
            SetRendererStates(renderer);
            renderer.dffRenderer.DFFModels[modelName].Render(renderer.Device);
        }

        [MiscSetting(underlyingType: MiscSettingUnderlyingType.Int)]
        public bool IsBlue { get; set; }
        [MiscSetting]
        public EStartMode StartMode { get; set; }
        [MiscSetting]
        public float Range { get; set; }
        [MiscSetting]
        public float Scale { get; set; }
        [MiscSetting(underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool IsUpsideDown { get; set; }
        [MiscSetting]
        public EBaseType BaseType { get; set; }
        [MiscSetting(underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool HasSE { get; set; }
        [MiscSetting(underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool HasCollision { get; set; }
    }
}
