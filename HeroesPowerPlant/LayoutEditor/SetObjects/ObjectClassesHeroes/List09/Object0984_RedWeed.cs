using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0984_RedWeed : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        [MiscSetting]
        public float StartRange { get; set; }
        [MiscSetting]
        public float Scale { get; set; }
    }
}