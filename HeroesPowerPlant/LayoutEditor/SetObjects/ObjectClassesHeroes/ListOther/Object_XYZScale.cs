using HeroesPowerPlant.Shared.Utilities;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_XYZScale : SetObjectHeroes
    {
        private readonly float scaleAdd;

        public Object_XYZScale(float scaleAdd)
        {
            this.scaleAdd = scaleAdd;
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX + scaleAdd, ScaleY + scaleAdd, ScaleZ + scaleAdd)
                * DefaultTransformMatrix();

            CreateBoundingBox();
        }

        public float ScaleX { get; set; }
        public float ScaleY { get; set; }
        public float ScaleZ { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            ScaleX = reader.ReadSingle();
            ScaleY = reader.ReadSingle();
            ScaleZ = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(ScaleX);
            writer.Write(ScaleY);
            writer.Write(ScaleZ);
        }
    }
}