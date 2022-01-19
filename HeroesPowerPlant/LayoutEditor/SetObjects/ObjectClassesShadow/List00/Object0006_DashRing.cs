using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0006_DashRing : SetObjectShadow
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
            Matrix.Translation(Position);
            CreateBoundingBox();
        }

        public float Strength
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }

        public float NoControlTime
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
    }
}
