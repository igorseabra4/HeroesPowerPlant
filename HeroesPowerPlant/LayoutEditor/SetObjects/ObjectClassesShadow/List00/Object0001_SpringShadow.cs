using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0001_SpringShadow : SetObjectShadow
    {
        public override void CreateTransformMatrix()
        {
            // function 800c9ed4 | RotationTemplateGen
            var shift = MathUtil.Pi / 180f;
            transformMatrix =
                Matrix.RotationY(0f) * // Spring only uses Y for launchDirection, no model rotation
                Matrix.RotationZ(Rotation.Z * shift) *
                Matrix.RotationX(Rotation.X * shift) *
                Matrix.Translation(Position);
            CreateBoundingBox();
        }

        [MiscSetting]
        public float Strength { get; set; }
        [MiscSetting]
        public float NoControlTime { get; set; }
    }
}
