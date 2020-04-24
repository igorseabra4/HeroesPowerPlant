using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0982_Mushroom : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            base.CreateTransformMatrix();
            transformMatrix = Matrix.Scaling(Scale) * transformMatrix;
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