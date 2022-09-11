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

        [MiscSetting]
        public EDirection DirectionType { get; set; }
        [MiscSetting]
        public EFanShape Shape { get; set; }
        [MiscSetting]
        public float Radius { get; set; }
        [MiscSetting, Description("Box shape only")]
        public float BoxTypeAirHeight { get; set; }
        [MiscSetting, Description("Cylinder shape only")]
        public float AirHeight { get; set; }
        [Description("Box shape only")]
        public float BoxRadius
        {
            get => AirHeight;
            set => AirHeight = value;
        }
        [MiscSetting]
        public float AirStrength { get; set; }
        [MiscSetting]
        public float TimeToRun { get; set; }
        [MiscSetting]
        public float TimeToRecharge { get; set; }
        [MiscSetting]
        public ENoYes HasModel { get; set; }
        public EFanRunning FanRunning
        {
            get => (EFanRunning)LinkIDMakeRun;
            set => LinkIDMakeRun = (int)value;
        }
        [MiscSetting, Description("FanRunning shares this, can set to LinkID to watch for")]
        public int LinkIDMakeRun { get; set; }
    }
}

