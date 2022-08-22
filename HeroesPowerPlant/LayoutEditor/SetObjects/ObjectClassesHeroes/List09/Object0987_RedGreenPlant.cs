using HeroesPowerPlant.Shared.Utilities;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0987_RedGreenPlant : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        public float Scale { get; set; }
        public int ObjectType { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Scale = reader.ReadSingle();
            ObjectType = reader.ReadInt32();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Scale);
            writer.Write(ObjectType);
        }
    }
}