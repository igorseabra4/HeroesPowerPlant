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

        [MiscSetting]
        public float Scale { get; set; }
    }
}