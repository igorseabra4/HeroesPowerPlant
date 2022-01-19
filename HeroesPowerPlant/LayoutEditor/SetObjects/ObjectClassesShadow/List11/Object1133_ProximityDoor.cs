using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object1133_ProximityDoor : SetObjectShadow {
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

        [Description("Open/Close Behavior")]
        public ProximityDoorLockType LockType {
            get => (ProximityDoorLockType)ReadInt(0);
            set => Write(0, (int)value);
        }

        [Description("ModelSelection")]
        public int Model {
            get => ReadInt(4);
            set => Write(4, value);
        }

        public float DetectRange_X {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("Negative values seem to cause issues, adds to positive direction only")]
        public float DetectRange_Y {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        [Description("Adds to radius in both directions")]
        public float DetectRange_Z {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        [Description("Detection offset on X Axis")]
        public float Offset_X {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        [Description("Detection offset on Y Axis")]
        public float Offset_Y {
            get => ReadFloat(24);
            set => Write(24, value);
        }

        [Description("Detection offset on Z Axis")]
        public float Offset_Z {
            get => ReadFloat(28);
            set => Write(28, value);
        }
    }

    public enum ProximityDoorLockType {
        ActivateWhenPlayerNear, //normal
        NotActivatedUntilLinkID //key
    }
}
