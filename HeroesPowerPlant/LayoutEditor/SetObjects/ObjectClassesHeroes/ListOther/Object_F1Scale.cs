using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_F1Scale : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale) * DefaultTransformMatrix();

            CreateBoundingBox();
        }

        public float Scale
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
    }
}