using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object0051_TriggerTalking : SetObjectShadow {
        //AKA SetHintCollision

        public TriggerShape Shape {
            get => (TriggerShape)ReadInt(0);
            set => Write(0, (int)value);
        }

        public float Size_X {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float Size_Y {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Size_Z {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public int AudioBranchID {
            get => ReadInt(16);
            set => Write(16, value);
        }

        public AudioBranchType AudioBranchType {
            get => (AudioBranchType)ReadInt(20);
            set => Write(20, (int)value);
        }

        public float float_06 {
            get => ReadFloat(24);
            set => Write(24, value);
        }

        public int int_07 {
            get => ReadInt(28);
            set => Write(28, value);
        }

        public float float_07 {
            get => ReadFloat(28);
            set => Write(28, value);
        }

        public int int_08 { //8 int | 0, 7 [can be null]
            get {
                if (MiscSettings.Length > 32)
                    return ReadInt(32);
                return -1;
            }
            set {
                if (MiscSettings.Length < 36)
                    return;
                Write(32, value);
            }
        }

        // 6 Does not appear on 2 objects throughout the game
        [Description("Set Disappear/Appear behavior when LinkID condition met.")]
        public TriggerLinkBehavior TriggerLinkBehavior {
            get {
                if (MiscSettings.Length > 36)
                    return (TriggerLinkBehavior)ReadInt(36);
                return (TriggerLinkBehavior)(-1);
            }
            set {
                if (MiscSettings.Length < 40)
                    return;
                Write(36, (int)value);
            }
        }

        [Description("Same slot as TriggerLinkBehavior, but one object may have this field as a float")]
        //9 int OR float // 0, 1 | float 140 | [can be null]
        public float float_09 {
            get {
                if (MiscSettings.Length > 36)
                    return ReadFloat(36);
                return -1;
            }
            set {
                if (MiscSettings.Length < 40)
                    return;
                Write(36, value);
            }
        }
    }
    public enum AudioBranchType {
        CurrentMissionPartner=-1,
        Dark=0,
        Normal=1,
        Hero=2
    }
}
