using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0019_ItemBalloon : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        [MiscSetting]
        public EItemHeroes Item { get; set; }
        [MiscSetting(padAfter: 4)]
        public float Scale { get; set; }
        [MiscSetting, Description("Usually 0")]
        public short Unknown { get; set; }
    }
}