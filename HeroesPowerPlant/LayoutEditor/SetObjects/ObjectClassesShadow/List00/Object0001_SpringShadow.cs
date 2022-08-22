using SharpDX;
using System.IO;

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

        public float Strength { get; set; }
        public float NoControlTime { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            Strength = reader.ReadSingle();
            NoControlTime = reader.ReadSingle();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write(Strength);
            writer.Write(NoControlTime);
        }
    }
}
