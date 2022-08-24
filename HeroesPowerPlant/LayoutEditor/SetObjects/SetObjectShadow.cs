using SharpDX;
using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class SetObjectShadow : SetObject
    {
        public override void SetMiscSettings(byte[] miscSettings)
        {
            using var reader = new BinaryReader(new MemoryStream(miscSettings));
            ReadMiscSettings(reader, miscSettings.Length);
        }

        public virtual void ReadMiscSettings(BinaryReader reader, int count)
        {
            ReadMiscSettings(reader);
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = DefaultTransformMatrix();
            CreateBoundingBox();
        }

        protected Matrix DefaultTransformMatrix(float yAddDeg = 0) =>
            Matrix.RotationZ(MathUtil.DegreesToRadians(Rotation.Z)) *
            Matrix.RotationX(MathUtil.DegreesToRadians(Rotation.X)) *
            Matrix.RotationY(MathUtil.DegreesToRadians(Rotation.Y + yAddDeg)) *
            Matrix.Translation(Position);
    }
}
