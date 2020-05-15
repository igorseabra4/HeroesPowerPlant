using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object0834_Tornado : SetObjectShadow {
        //Tornado
        public TornadoTurn SpinDirection {
            get => (TornadoTurn)ReadInt(0);
            set => Write(0, (int)value);
        }

        [Description("m/s")]
        public float SpinningSpeed {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        [Description("m/s")]
        public float UpSpeed {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("m")]
        public float Cylinder_Radius {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        [Description("m")]
        public float Cylinder_Height {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        [Description("m")]
        public float Cylinder_Offset_Y {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        [Description("deg")]
        public float Launch_X {
            get => ReadFloat(24);
            set => Write(24, value);
        }

        [Description("deg")]
        public float Launch_Y {
            get => ReadFloat(28);
            set => Write(28, value);
        }

        [Description("m/s")]
        public float Launch_Speed {
            get => ReadFloat(32);
            set => Write(32, value);
        }

        [Description("sec")]
        public float NoControlSec {
            get => ReadFloat(36);
            set => Write(36, value);
        }
    }

    public enum TornadoTurn {
        Left,
        Right
    }
}
