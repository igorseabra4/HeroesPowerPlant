using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1305_EggFleetDoor : SetObjectHeroes
    {
        private Matrix triggerMatrix;

        public override void CreateTransformMatrix()
        {
            base.CreateTransformMatrix();

            triggerMatrix = Matrix.Scaling(TriggerSizeX, TriggerSizeY, TriggerSizeZ) *
                Matrix.RotationY(TriggerRotY) *
                Matrix.Translation(TriggerX, TriggerY, TriggerZ);

            CreateBoundingBox();
        }

        public override void Draw(SharpRenderer renderer)
        {
            base.Draw(renderer);

            if (isSelected)
                renderer.DrawCubeTrigger(triggerMatrix, true);
        }

        [MiscSetting]
        public float TriggerX { get; set; }
        [MiscSetting]
        public float TriggerY { get; set; }
        [MiscSetting]
        public float TriggerZ { get; set; }
        [MiscSetting]
        public short TriggerSizeX { get; set; }
        [MiscSetting]
        public short TriggerSizeY { get; set; }
        [MiscSetting]
        public short TriggerSizeZ { get; set; }
        [MiscSetting]
        public short TriggerRotY { get; set; }
    }
}