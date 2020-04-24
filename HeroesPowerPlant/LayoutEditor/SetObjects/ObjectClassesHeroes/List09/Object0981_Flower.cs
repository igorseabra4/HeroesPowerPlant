using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0981_Flower : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            base.CreateTransformMatrix();
            transformMatrix = Matrix.Scaling(Scale) * transformMatrix;
            CreateBoundingBox();
        }

        public float StartRange
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float Scale
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public int FlowerType
        {
            get => ReadInt(12);
            set => Write(12, value);
        }
    }
}