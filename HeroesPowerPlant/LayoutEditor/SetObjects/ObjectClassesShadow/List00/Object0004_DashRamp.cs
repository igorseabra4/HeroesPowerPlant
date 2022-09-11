using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0004_DashRamp : SetObjectShadow
    {
        public override void CreateTransformMatrix()
        {
            // function 800c9ed4 | RotationTemplateGen
            var shift = MathUtil.Pi / 180f;
            transformMatrix =
                Matrix.RotationZ(Rotation.Z * shift) *
                Matrix.RotationX(Rotation.X * shift) *
                Matrix.RotationY(Rotation.Y * shift) *
                Matrix.Translation(Position);
            CreateBoundingBox();
        }

        [MiscSetting]
        public float Strength { get; set; }
        [MiscSetting]
        public float Height { get; set; }
        [MiscSetting]
        public float NoControlTime { get; set; }
    }
}
