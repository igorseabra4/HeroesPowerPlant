using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_IntTypeFloatScale : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale) * DefaultTransformMatrix();

            CreateBoundingBox();
        }

        [MiscSetting]
        public int ObjectType { get; set; }
        [MiscSetting]
        public float Scale { get; set; }
    }
}