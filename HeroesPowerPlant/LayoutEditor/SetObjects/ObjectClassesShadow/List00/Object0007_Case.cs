using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0007_Case : SetObjectShadow
    {
        public enum ECaseType : int
        {
            BlackArms,
            GUN
        }

        [MiscSetting]
        public ECaseType CaseType { get; set; }
        [MiscSetting]
        public float ScaleX { get; set; }
        [MiscSetting]
        public float ScaleY { get; set; }
        [MiscSetting]
        public float ScaleZ { get; set; }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ) *
                DefaultTransformMatrix();
            CreateBoundingBox();
        }
    }
}

