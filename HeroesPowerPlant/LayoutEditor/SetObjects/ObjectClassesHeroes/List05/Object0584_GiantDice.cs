using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0584_GiantDice : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale) * DefaultTransformMatrix();

            CreateBoundingBox();
        }

        [MiscSetting]
        public int ObjectType { get; set; }
        [MiscSetting]
        public int Speed { get; set; }
        [MiscSetting]
        public float Scale { get; set; }
        [MiscSetting]
        public int Block { get; set; }
    }
}