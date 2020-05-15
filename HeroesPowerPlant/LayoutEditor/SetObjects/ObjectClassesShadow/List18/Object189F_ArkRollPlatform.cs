using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object189F_ArkRollPlatform : SetObjectShadow {
        //FootingRoll(Model{0=Small,1=Large}, MoveLengthX point, MoveLengthY point, MoveLengthZ point,
            //MoveSec, MovePauseSec, RotType{0=OneWay,1=RT}, RotAxis{0=X,1=Y,2=Z}, RotSpd deg/sec, RotMax(RT Only) deg)
        public ArkRollModel Model {
            get => (ArkRollModel)ReadInt(0);
            set => Write(0, (int)value);
        }

        [Description("point")]
        public float MoveLengthX {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        [Description("point")]
        public float MoveLengthY {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("point")]
        public float MoveLengthZ {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        [Description("sec")]
        public float MoveSec {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        [Description("sec")]
        public float WaitSec {
            get => ReadFloat(20);
            set => Write(20, value);
        }
        public ArkRollRotType RotationType {
            get => (ArkRollRotType)ReadInt(24);
            set => Write(24, (int)value);
        }
        public ArkRollRotAxis RotationAxis {
            get => (ArkRollRotAxis)ReadInt(28);
            set => Write(28, (int)value);
        }

        [Description("deg/sec")]
        public float RotationSpeed {
            get => ReadFloat(32);
            set => Write(32, value);
        }
        [Description("RotationType RT only; deg")]
        public float RotationMax {
            get => ReadFloat(36);
            set => Write(36, value);
        }
    }

    public enum ArkRollModel {
        Small,
        Large
    }
    public enum ArkRollRotType {
        OneWay,
        RT
    }

    public enum ArkRollRotAxis {
        X,
        Y,
        Z
    }
}
