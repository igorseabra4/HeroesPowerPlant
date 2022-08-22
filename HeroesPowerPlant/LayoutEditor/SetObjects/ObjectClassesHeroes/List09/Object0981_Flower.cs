using HeroesPowerPlant.Shared.Utilities;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0981_Flower : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        public float StartRange { get; set; }
        public float Scale { get; set; }
        public int ObjectType { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            StartRange = reader.ReadSingle();
            Scale = reader.ReadSingle();
            ObjectType = reader.ReadInt32();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(StartRange);
            writer.Write(Scale);
            writer.Write(ObjectType);
        }
    }
}