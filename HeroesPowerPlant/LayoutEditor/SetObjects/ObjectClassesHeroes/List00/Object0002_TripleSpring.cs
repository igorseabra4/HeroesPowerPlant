using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0002_TripleSpring : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f, 1f, 1f) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        [MiscSetting, Description("Defaults to 5.0")]
        public float Power { get; set; }
        [MiscSetting, Description("In frames")]
        public float Scale { get; set; }
        [MiscSetting]
        public short NoControlTime { get; set; }
        [MiscSetting]
        public EItemHeroes Item { get; set; }
    }
}
