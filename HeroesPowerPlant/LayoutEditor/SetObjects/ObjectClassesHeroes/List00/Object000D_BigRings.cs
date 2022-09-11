using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000D_BigRings : SetObjectHeroes
    {
        public enum EType : short
        {
            Speed = 0,
            FlyA = 1,
            FlyB = 2,
            PowerS = 3,
            PowerL = 4
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = DefaultTransformMatrix(MathUtil.Pi);
            CreateBoundingBox();
        }

        [MiscSetting]
        public EType RingsType { get; set; }
        [MiscSetting, Description("In frames")]
        public short AdditionalControlTime { get; set; }
        [MiscSetting, Description("Defaults to 5.0")]
        public float Speed { get; set; }
        [MiscSetting]
        public float Offset { get; set; }
    }
}