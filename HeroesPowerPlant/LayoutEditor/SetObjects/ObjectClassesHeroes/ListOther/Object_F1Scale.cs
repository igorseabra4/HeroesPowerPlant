using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_F1Scale : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            base.CreateTransformMatrix();
            transformMatrix = Matrix.Scaling(Scale == 0f ? 1f : Scale) * transformMatrix;

            CreateBoundingBox();
        }

        public float Scale
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
    }
}