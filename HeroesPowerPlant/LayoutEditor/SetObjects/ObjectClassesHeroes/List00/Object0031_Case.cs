using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0031_Case : SetObjectHeroes
    {
        public enum EDirection : byte
        {
            Up = 0,
            Down = 1,
        }
        
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling((ScaleX + 20f) * 0.05f, (ScaleY + 20f) * 0.05f, (ScaleZ + 20f) * 0.05f) * Matrix.RotationX(Direction == EDirection.Down ? MathUtil.Pi : 0) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        [MiscSetting]
        public float ScaleX { get; set; }
        [MiscSetting]
        public float ScaleY { get; set; }
        [MiscSetting]
        public float ScaleZ { get; set; }
        [MiscSetting, Description("Doesn't use actual Link ID. Use this one.")]
        public byte LinkID { get; set; }
        [MiscSetting]
        public EDirection Direction { get; set; }
    }
}
