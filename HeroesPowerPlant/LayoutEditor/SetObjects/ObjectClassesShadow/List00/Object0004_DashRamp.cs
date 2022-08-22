using SharpDX;
using System.IO;

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

        public float Strength { get; set; }
        public float Height { get; set; }
        public float NoControlTime { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            Strength = reader.ReadSingle();
            Height = reader.ReadSingle();
            NoControlTime = reader.ReadSingle();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write(Strength);
            writer.Write(Height);
            writer.Write(NoControlTime);
        }
    }
}
