using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0180_FlowerPatch : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale) * DefaultTransformMatrix();

            CreateBoundingBox();
        }

        [MiscSetting]
        public byte ObjectType { get; set; }
        [MiscSetting]
        public float Scale { get; set; }
    }
}
