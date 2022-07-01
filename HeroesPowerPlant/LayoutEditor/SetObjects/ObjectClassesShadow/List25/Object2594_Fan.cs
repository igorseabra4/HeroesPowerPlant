using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object2594_Fan : SetObjectShadow
    {

        public override void Draw(SharpRenderer renderer)
        {
            base.Draw(renderer);
            if (isSelected)
                renderer.DrawCubeTrigger(CreateAffectedTransformMatrix(), isSelected, new Color4(1f, 0.75f, 0.79f, 0.5f));
        }

        private Matrix CreateAffectedTransformMatrix()
        {
            Matrix triggerTransformMatrix = Matrix.Scaling(Radius * 2);
            triggerTransformMatrix *= DefaultTransformMatrix();
            return triggerTransformMatrix;
        }

        public CommonDirectionType FanType
        { //0 or 1
            get => (CommonDirectionType)ReadInt(0);
            set => Write(0, (int)value);
        }

        public FanForm FanForm
        { //0
            get => (FanForm)ReadInt(4);
            set => Write(4, (int)value);
        }

        public float Radius
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("Only for FanForm BoxType")]
        public float BoxTypeAirHeight
        { //always 0
            get => ReadFloat(12);
            set => Write(12, value);
        }

        [Description("Cylinder Type Air Height; Box Type Radius")]
        public float AirHeightANDBoxTypeRadius
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float AirStrength
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public float TimeToRun
        {
            get => ReadFloat(24);
            set => Write(24, value);
        }

        public float TimeToRecharge
        {
            get => ReadFloat(28);
            set => Write(28, value);
        }

        public CommonNoYes HasModel
        { //0 or 1
            get => (CommonNoYes)ReadInt(32);
            set => Write(32, (int)value);
        }

        public FanRunning FanRunning
        { //-1 or 255
            get => (FanRunning)ReadInt(36);
            set => Write(36, (int)value);
        }

        [Description("FanRunning shares this, can set to LinkID to watch for")]
        public int LinkIDMakeRun
        {
            get => ReadInt(36);
            set => Write(36, value);
        }
    }

    public enum FanForm
    {
        Cylinder,
        Box,
    }

    public enum FanRunning
    {
        Yes = -1,
        No = 255
    }
}

