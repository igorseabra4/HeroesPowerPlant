using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000F_DashRamp : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = DefaultTransformMatrix(MathUtil.Pi);
            CreateBoundingBox();
        }

        [Description("Defaults to 5.0")]
        public float SpeedHorizontal
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        [Description("Defaults to 5.0")]
        public float SpeedVertical
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("In frames")]
        public short NoControlTime
        {
            get => ReadShort(12);
            set => Write(12, value);
        }
    }
}