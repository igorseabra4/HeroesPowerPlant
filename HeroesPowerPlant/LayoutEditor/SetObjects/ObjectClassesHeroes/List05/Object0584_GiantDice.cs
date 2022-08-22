using HeroesPowerPlant.Shared.Utilities;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0584_GiantDice : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale) * DefaultTransformMatrix();

            CreateBoundingBox();
        }

        public int ObjectType { get; set; }
        public int Speed { get; set; }
        public float Scale { get; set; }
        public int Block { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            ObjectType = reader.ReadInt32();
            Speed = reader.ReadInt32();
            Scale = reader.ReadSingle();
            Block = reader.ReadInt32();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(ObjectType);
            writer.Write(Speed);
            writer.Write(Scale);
            writer.Write(Block);
        }
    }
}