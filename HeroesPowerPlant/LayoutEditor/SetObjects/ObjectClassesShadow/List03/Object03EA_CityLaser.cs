using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object03EA_CityLaser : SetObjectShadow
    {
        [MiscSetting]
        public EYesNo HurtPlayer { get; set; }
        [MiscSetting]
        public float DetectRadius { get; set; }
        [MiscSetting]
        public float Delay { get; set; }

        public override void Draw(SharpRenderer renderer)
        {
            base.Draw(renderer);
            if (isSelected)
                renderer.DrawSphereTrigger(CreateTriggerTransformMatrix(), isSelected, new Color4(1f, 0.75f, 0.79f, 0.5f));
        }

        private Matrix CreateTriggerTransformMatrix()
        {
            Matrix triggerTransformMatrix = Matrix.Scaling(DetectRadius * 15);
            triggerTransformMatrix *= DefaultTransformMatrix();
            return triggerTransformMatrix;
        }
    }
}

