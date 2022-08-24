using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07DF_LightspeedFirewall : SetObjectShadow
    {
        //ElecFireWall(SearchRange, InitialY)

        [MiscSetting]
        public float Scale_X { get; set; }
        [MiscSetting]
        public float Scale_Y { get; set; }
        [MiscSetting]
        public float Scale_Z { get; set; }
        [MiscSetting]
        public float SearchRange { get; set; }
        [MiscSetting, Description("Move object starting point/appearance on the Y axis this amount.\n Animates to original position on detect")]
        public float Initial_Y { get; set; }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale_X, Scale_Y, Scale_Z);
            transformMatrix *= DefaultTransformMatrix();
            CreateBoundingBox();
        }
    }
}
