using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1184_SmokeScreen : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = IsUpsideDown ? Matrix.RotationY(MathUtil.Pi) : Matrix.Identity *
                DefaultTransformMatrix();

            CreateBoundingBox();
        }

        [MiscSetting]
        public int ObjectType { get; set; }
        [MiscSetting]
        public float Speed { get; set; }
        [MiscSetting(underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool IsUpsideDown { get; set; }
    }
}
