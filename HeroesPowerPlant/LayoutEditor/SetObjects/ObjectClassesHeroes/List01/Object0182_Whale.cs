using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0182_Whale : SetObjectHeroes
    {
        private Matrix triggerMatrix;

        public override void CreateTransformMatrix()
        {
            base.CreateTransformMatrix();

            triggerMatrix = Matrix.Scaling(TriggerSize) * Matrix.Translation(TriggerX, TriggerY, TriggerZ);
        }

        public override void Draw(SharpRenderer renderer)
        {
            base.Draw(renderer);

            if (isSelected)
                renderer.DrawSphereTrigger(triggerMatrix, true);
        }

        [MiscSetting]
        public byte WhaleType { get; set; }
        [MiscSetting]
        public short TriggerSize { get; set; }
        [MiscSetting]
        public float WhaleScale { get; set; }
        [MiscSetting]
        public float ArchRadius { get; set; }
        [MiscSetting]
        public float TriggerX { get; set; }
        [MiscSetting]
        public float TriggerY { get; set; }
        [MiscSetting]
        public float TriggerZ { get; set; }
    }
}
