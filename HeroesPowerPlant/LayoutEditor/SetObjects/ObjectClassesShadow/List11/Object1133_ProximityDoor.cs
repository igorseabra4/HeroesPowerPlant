using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object1133_ProximityDoor : SetObjectShadow {
        //SetBaseDoor(type: normal/key, model, width, height, depth)
        //SetDoor(type, model, width, height, depth)
        // Enum type { normal, key }

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

        public float DetectRange_Y {
            get => ReadFloat(12);
            set => Write(12, value);
        }

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
