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

        public float ScaleX
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float ScaleY
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