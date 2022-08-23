using HeroesPowerPlant.Shared.Utilities;
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

        public bool IsBlue { get; set; }
        public EStartMode StartMode { get; set; }
        public float Range { get; set; }
        public float Scale { get; set; }
        public bool IsUpsideDown { get; set; }
        public EBaseType BaseType { get; set; }
        public bool HasSE { get; set; }
        public bool HasCollision { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            IsBlue = reader.ReadInt32Bool();
            StartMode = (EStartMode)reader.ReadInt32();
            Range = reader.ReadSingle();
            Scale = reader.ReadSingle();
            IsUpsideDown = reader.ReadByteBool();
            BaseType = (EBaseType)reader.ReadByte();
            HasSE = reader.ReadByteBool();
            HasCollision = reader.ReadByteBool();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(IsBlue ? 1 : 0);
            writer.Write((int)StartMode);
            writer.Write(Range);
            writer.Write(Scale);
            writer.Write((byte)(IsUpsideDown ? 1 : 0));
            writer.Write((byte)BaseType);
            writer.Write((byte)(HasSE ? 1 : 0));
            writer.Write((byte)(HasCollision ? 1 : 0));
        }
    }
}
