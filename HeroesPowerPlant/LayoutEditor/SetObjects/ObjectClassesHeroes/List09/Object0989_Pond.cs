using HeroesPowerPlant.Shared.Utilities;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0989_Pond : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, 1, ScaleZ) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        public int ObjectType { get; set; }
        public float ScaleX { get; set; }
        public float ScaleZ { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            ObjectType = reader.ReadInt32();
            ScaleX = reader.ReadSingle();
            ScaleZ = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(ObjectType);
            writer.Write(ScaleX);
            writer.Write(ScaleZ);
        }
    }
}