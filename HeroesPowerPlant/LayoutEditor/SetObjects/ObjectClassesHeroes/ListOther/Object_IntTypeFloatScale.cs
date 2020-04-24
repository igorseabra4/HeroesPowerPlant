using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_IntTypeFloatScale : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            base.CreateTransformMatrix();
            transformMatrix = Matrix.Scaling(Scale == 0 ? 1 : Scale) * transformMatrix;

            CreateBoundingBox();
        }

        public int ObjectType
        {
            get => ReadInt(4);
            set => Write(4, value);
        }

        public float Scale
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}