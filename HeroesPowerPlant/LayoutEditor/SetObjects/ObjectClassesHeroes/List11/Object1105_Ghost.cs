using HeroesPowerPlant.Shared.Utilities;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1105_Ghost : SetObjectHeroes
    {
        public enum EGhostType : int
        {
            NoMove = 0,
            Line = 1,
            Circle = 2
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f) * DefaultTransformMatrix();

            CreateBoundingBox();
        }

        public EGhostType GhostType { get; set; }
        public float Range { get; set; }
        public float MovingArea { get; set; }
        public float Speed { get; set; }
        public float Scale { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            GhostType = (EGhostType)reader.ReadInt32();
            Range = reader.ReadSingle();
            MovingArea = reader.ReadSingle();
            Speed = reader.ReadSingle();
            Scale = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((int)GhostType);
            writer.Write(Range);
            writer.Write(MovingArea);
            writer.Write(Speed);
            writer.Write(Scale);
        }
    }
}
