using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object001A_Wind : SetObjectShadow
    {
        // Technically a copy of "Fan", with a unique model

        public override void Draw(SharpRenderer renderer)
        {
            base.Draw(renderer);
            if (isSelected)
                renderer.DrawCubeTrigger(CreateAffectedTransformMatrix(), isSelected, new Color4(1f, 0.75f, 0.79f, 0.5f));
        }

        private Matrix CreateAffectedTransformMatrix()
        {
            Matrix affectedTransformMatrix = Matrix.Scaling(AirHeight * 2, BoxTypeAirHeight * 2, AirHeight * 2);
            affectedTransformMatrix *= Matrix.Translation(0f, BoxTypeAirHeight, 0f);
            affectedTransformMatrix *= DefaultTransformMatrix();
            return affectedTransformMatrix;
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
        public EFanRunning WindBlowing //-1 or 255
        {
            get => (EFanRunning)LinkIDMakeRun;
            set => LinkIDMakeRun = (int)value;
        }
        [MiscSetting, Description("WindBlowing shares this, can set to LinkID to watch for")]
        public int LinkIDMakeRun { get; set; }
    }
}

