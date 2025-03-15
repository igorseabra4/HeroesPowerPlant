using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object14B6_GravityChangeCollision : SetObjectShadow
    {
        [MiscSetting, Description("Size of detection on X axis\nSpreads evenly in both directions from position")]
        public float Size_X { get; set; }
        [MiscSetting, Description("Size of detection on Y axis\nSpreads evenly in both directions from position")]
        public float Size_Y { get; set; }
        [MiscSetting, Description("Size of detection on Z axis\nSpreads evenly in both directions from position")]
        public float Size_Z { get; set; }
        [MiscSetting, Description("Sets the base gravity. By default the world is NegY")]
        public EGravityDirection GravityDirection { get; set; }

        public override void Draw(SharpRenderer renderer)
        {
            base.Draw(renderer);
            if (isSelected)
                renderer.DrawCubeTrigger(CreateTriggerTransformMatrix(), isSelected, new Color4(0f, 0.75f, 0.79f, 0.5f));
        }

        private Matrix CreateTriggerTransformMatrix()
        {
            Matrix triggerTransformMatrix = Matrix.Scaling(Size_X * 2, Size_Y * 2, Size_Z * 2);
            triggerTransformMatrix *= DefaultTransformMatrix();
            return triggerTransformMatrix;
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Size_X * 2, (Size_Y + Size_Z) * 2, Size_X * 2);
            transformMatrix *= Matrix.RotationX(90 * (MathUtil.Pi / 180));
            transformMatrix *= DefaultTransformMatrix();
            CreateBoundingBox();
        }
    }
}
