using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object0050_Trigger : SetObjectShadow {

        //SetCollision

        public TriggerType TriggerType {
            get => (TriggerType)ReadInt(0);
            set => Write(0, (int)value);
        }

        public TriggerShape Shape {
            get => (TriggerShape)ReadInt(4);
            set => Write(4, (int)value);
        }

        public float Size_X {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Size_Y {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float Size_Z {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        [Description("LinkIDTrigger's LinkID to Activate OR LinkID to watch in other types")]
        //Disappear
        public int Affect_LinkID { //5
            get => ReadInt(20);
            set => Write(20, value);
        }

        // 6 Does not appear on 2 objects throughout the game
        [Description("Set Disappear/Appear behavior when LinkID condition met.")]
        public TriggerLinkBehavior TriggerLinkBehavior { //6
            get {
                if (MiscSettings.Length > 24)
                    return (TriggerLinkBehavior)ReadInt(24);
                return (TriggerLinkBehavior)(-1);
            }
            set {
                if (MiscSettings.Length < 28)
                    return;
                Write(24, (int)value);
            }
        }
    }

    public enum TriggerType {
        SolidCollision=0,
        LinkIDTrigger=2,
        HurtPlayer=3,
        KillPlayer=4,
        ChaosControlCancelOn=5,
        ChaosControlCancelOff=6,
        ChaosControlStop=7,
        MaintainBehaviorSkydive=9,
        LockControlsWhileInTrigger=10,
        CompleteMission=11
    }

    public enum TriggerShape {
        Cylinder,
        Cube,
        Cone
    }

    public enum TriggerLinkBehavior {
        NotValidInObject = -1,
        Disappear = 0,
        Appear = 1
    }
}
