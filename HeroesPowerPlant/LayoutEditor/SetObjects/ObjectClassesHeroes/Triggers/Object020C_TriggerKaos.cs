using HeroesPowerPlant.Shared.Utilities;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object020C_TriggerKaos : SetObjectHeroes
    {
        private BoundingSphere sphereBound;

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale) * DefaultTransformMatrix();
            sphereBound = new BoundingSphere(Position, Scale);
            boundingBox = BoundingBox.FromSphere(sphereBound);
        }

        public override void Draw(SharpRenderer renderer)
        {
            renderer.DrawSphereTrigger(transformMatrix, isSelected);
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            return r.Intersects(ref sphereBound, out distance);
        }

        public float Scale { get; set; }
        public byte KaosType { get; set; }
        public byte Param2 { get; set; }
        public float Param3 { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Scale = reader.ReadSingle();
            KaosType = reader.ReadByte();
            Param2 = reader.ReadByte();
            reader.BaseStream.Position += 2;
            Param3 = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Scale);
            writer.Write(KaosType);
            writer.Write(Param2);
            writer.Pad(2);
            writer.Write(Param3);
        }
    }
}
