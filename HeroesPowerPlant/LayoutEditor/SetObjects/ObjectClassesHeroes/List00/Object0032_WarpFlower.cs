using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0032_WarpFlower : SetObjectHeroes
    {
        public enum EFlowerType : byte
        {
            Item = 0,
            Scaffold = 1,
            Warp = 2
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        [MiscSetting]
        public EFlowerType FlowerType { get; set; }
        [MiscSetting]
        public float Scale { get; set; }
        [MiscSetting]
        public float RisingHeight { get; set; }
    }
}