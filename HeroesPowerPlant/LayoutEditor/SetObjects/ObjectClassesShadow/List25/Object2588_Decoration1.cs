using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object2588_Decoration1 : SetObjectShadow
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        [MiscSetting]
        public int DecorationType { get; set; }

        [MiscSetting]
        public float ScaleX { get; set; }

        [MiscSetting]
        public float ScaleY { get; set; }

        [MiscSetting]
        public float ScaleZ { get; set; }
    }
}
