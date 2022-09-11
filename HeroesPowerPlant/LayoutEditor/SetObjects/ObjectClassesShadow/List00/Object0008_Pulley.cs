using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0008_Pulley : SetObjectShadow
    {
        // Real name UpDownReel / UD_REEL::length,angle,power

        public override void CreateTransformMatrix()
        {
            // function 800c9ed4 | RotationTemplateGen
            var shift = MathUtil.Pi / 180f;
            transformMatrix =
            Matrix.RotationX(0f) *
            Matrix.RotationY(Rotation.Y * shift) * // UpDownReel only uses Y for model
            Matrix.RotationZ(0f) *
            Matrix.Translation(Position);
            Matrix.Translation(Position);
            CreateBoundingBox();
        }

        [MiscSetting]
        public float StartingLength { get; set; }
        [MiscSetting]
        public float EndingLength { get; set; }
        [MiscSetting]
        public float LetGoAngle { get; set; }
        [MiscSetting]
        public float LetGoLaunchForce { get; set; }
    }
}

