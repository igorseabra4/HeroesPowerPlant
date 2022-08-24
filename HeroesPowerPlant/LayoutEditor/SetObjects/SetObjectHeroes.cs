using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class SetObjectHeroes : SetObject
    {
        public override void SetMiscSettings(byte[] miscSettings)
        {
            using var reader = new EndianBinaryReader(new MemoryStream(miscSettings), Endianness.Big);
            ReadMiscSettings(reader);
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = DefaultTransformMatrix();
            CreateBoundingBox();
        }

        public Matrix DefaultTransformMatrix(float yAdd = 0) =>
            Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y) + yAdd) *
            Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
            Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
            Matrix.Translation(Position);
    }
}

