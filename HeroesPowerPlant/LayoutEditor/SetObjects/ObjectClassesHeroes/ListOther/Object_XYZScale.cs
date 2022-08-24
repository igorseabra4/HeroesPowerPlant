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

        [MiscSetting]
        public float ScaleX { get; set; }
        [MiscSetting]
        public float ScaleY { get; set; }
        [MiscSetting]
        public float ScaleZ { get; set; }
    }
}