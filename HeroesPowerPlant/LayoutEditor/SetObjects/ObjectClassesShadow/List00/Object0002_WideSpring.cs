using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0002_WideSpring : SetObjectShadow
    {
        // Previously known as WideSpring, official name is LongSpring
        public override void CreateTransformMatrix()
        {
            // function 800c9ed4 | RotationTemplateGen
            var shift = MathUtil.Pi / 180f;
            // X = +45 degree before any calculation

            // not YZX, YXZ, ZXY, ZYX, XZY, XYZ, 

            // NOT correct
            // Z is not first param?
            transformMatrix =
                Matrix.RotationZ(Rotation.Z * shift) *
                Matrix.RotationX((Rotation.X * shift) + 45f) *
                Matrix.RotationY(Rotation.Y * shift) *
                /*                Matrix.RotationY(Rotation.Y * shift) *
                                Matrix.RotationX((Rotation.X * shift) + 45f) *
                                Matrix.RotationZ(Rotation.Z * shift) **/

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
