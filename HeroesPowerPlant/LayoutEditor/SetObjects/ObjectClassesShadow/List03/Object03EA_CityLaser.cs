using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object03EA_CityLaser : SetObjectShadow
    {
        public CommonYesNo HurtPlayer
        {
            get => (CommonYesNo)ReadInt(0);
            set => Write(0, (int)value);
        }

        public float DetectRadius
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float Delay
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

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

