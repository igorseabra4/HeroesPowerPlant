using SharpDX;
using System.IO;

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

        public float StartingLength { get; set; }
        public float EndingLength { get; set; }
        public float LetGoAngle { get; set; }
        public float LetGoLaunchForce { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            StartingLength = reader.ReadSingle();
            EndingLength = reader.ReadSingle();
            LetGoAngle = reader.ReadSingle();
            LetGoLaunchForce = reader.ReadSingle();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write(StartingLength);
            writer.Write(EndingLength);
            writer.Write(LetGoAngle);
            writer.Write(LetGoLaunchForce);
        }
    }
}

