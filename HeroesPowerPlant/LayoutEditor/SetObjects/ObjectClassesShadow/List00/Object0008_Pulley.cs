using SharpDX;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object0008_Pulley : SetObjectShadow {

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

        public float StartingLength {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float EndingLength {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float LetGoAngle {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float LetGoLaunchForce {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}

