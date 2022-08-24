using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1133_ProximityDoor : SetObjectShadow
    {
        public enum ProximityDoorLockType : int
        {
            ActivateWhenPlayerNear, //normal
            NotActivatedUntilLinkID //key
        }

        //SetBaseDoor(type: normal/key, model, width, height, depth)
        //SetDoor(type, model, width, height, depth)
        // Enum type { normal, key }

        public override void Draw(SharpRenderer renderer)
        {
            base.Draw(renderer);
            if (isSelected)
                renderer.DrawCubeTrigger(CreateTriggerTransformMatrix(), isSelected, new Color4(1f, 0.75f, 0.79f, 0.5f));
        }

        private Matrix CreateTriggerTransformMatrix()
        {
            Matrix triggerTransformMatrix = Matrix.Scaling(DetectRange_X * 2, DetectRange_Y * 2, DetectRange_Z * 2);
            triggerTransformMatrix *= Matrix.Translation(0f, DetectRange_Y, 0f);
            triggerTransformMatrix *= Matrix.Translation(Offset_X, Offset_Y, Offset_Z);
            triggerTransformMatrix *= DefaultTransformMatrix();
            return triggerTransformMatrix;
        }

        [MiscSetting, Description("Open/Close Behavior")]
        public ProximityDoorLockType LockType { get; set; }
        [MiscSetting, Description("ModelSelection")]
        public int Model { get; set; }
        [MiscSetting]
        public float DetectRange_X { get; set; }
        [MiscSetting, Description("Negative values seem to cause issues, adds to positive direction only")]
        public float DetectRange_Y { get; set; }
        [MiscSetting, Description("Adds to radius in both directions")]
        public float DetectRange_Z { get; set; }
        [MiscSetting, Description("Detection offset on X Axis")]
        public float Offset_X { get; set; }
        [MiscSetting, Description("Detection offset on Y Axis")]
        public float Offset_Y { get; set; }
        [MiscSetting, Description("Detection offset on Z Axis")]
        public float Offset_Z { get; set; }
    }
}
