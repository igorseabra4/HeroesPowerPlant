using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0987_RedGreenPlant : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        [MiscSetting]
        public float Scale { get; set; }
        [MiscSetting]
        public int ObjectType { get; set; }
    }
}