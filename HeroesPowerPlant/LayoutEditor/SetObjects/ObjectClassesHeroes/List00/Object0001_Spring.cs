using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0001_Spring : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = DefaultTransformMatrix(MathUtil.Pi);
            CreateBoundingBox();
        }

        [MiscSetting, Description("Defaults to 5.0")]
        public float Power { get; set; }
        [MiscSetting, Description("In frames")]
        public short NoControlTime { get; set; }
        [MiscSetting]
        public float GuideLine { get; set; }
    }
}
