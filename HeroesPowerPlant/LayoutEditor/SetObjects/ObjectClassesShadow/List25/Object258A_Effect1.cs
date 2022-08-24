using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object258A_Effect1 : SetObjectShadow
    {
        [MiscSetting]
        public int EffectType { get; set; }

        [MiscSetting]
        public float ScaleX { get; set; }

        [MiscSetting]
        public float ScaleY { get; set; }

        [MiscSetting]
        public float ScaleZ { get; set; }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ);
            transformMatrix *= DefaultTransformMatrix();
            CreateBoundingBox();
        }
    }
}

