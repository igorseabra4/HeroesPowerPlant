using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object1133_ProximityDoor : SetObjectShadow {
        //SetDoor(type, model, width, height, depth, DOOR(model, speed, range)
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

        public float Detect_X {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Detect_Y {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float Detect_Z {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        [Description("")]
        public float Unknown5 {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        [Description("")]
        public float Speed {
            get => ReadFloat(24);
            set => Write(24, value);
        }

        [Description("")]
        public float Range {
            get => ReadFloat(28);
            set => Write(28, value);
        }
    }

    public enum ProximityDoorLockType {
        OpenWhenPlayerNear, //normal
        LockedUntilLinkID //key
    }
}
