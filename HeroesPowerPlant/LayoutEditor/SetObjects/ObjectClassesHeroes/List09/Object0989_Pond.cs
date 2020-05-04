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

        public int ObjectType
        {
            get => ReadInt(4);
            set => Write(4, value);
        }
        public float ScaleX
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float ScaleZ
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}