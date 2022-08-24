using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0989_Pond : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, 1, ScaleZ) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        [MiscSetting]
        public int ObjectType { get; set; }
        [MiscSetting]
        public float ScaleX { get; set; }
        [MiscSetting]
        public float ScaleZ { get; set; }
    }
}