using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000F_DashRamp : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = DefaultTransformMatrix(MathUtil.Pi);
            CreateBoundingBox();
        }

        [MiscSetting, Description("Defaults to 5.0")]
        public float SpeedHorizontal { get; set; }
        [MiscSetting, Description("Defaults to 5.0")]
        public float SpeedVertical { get; set; }
        [MiscSetting, Description("In frames")]
        public short NoControlTime { get; set; }
    }
}