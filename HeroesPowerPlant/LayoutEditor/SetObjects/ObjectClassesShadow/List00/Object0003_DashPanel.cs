using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0003_DashPanel : Object0001_SpringShadow
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
    }
}
