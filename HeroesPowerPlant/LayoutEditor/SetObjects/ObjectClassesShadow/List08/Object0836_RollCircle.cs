using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object0836_RollCircle : SetObjectShadow {
        //Tornado Flying Objects
        //RollCircle 
        public RollCircleModel Model {
            get => (RollCircleModel)ReadInt(0);
            set => Write(0, (int)value);
        }

        [Description("Number of objects orbiting")]
        public int NumberOfObjects {
            get => ReadInt(4);
            set => Write(4, value);
        }

        [Description("m")]
        public float Radius {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("deg/sec")]
        public float CircleSpeed {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        [Description("deg/sec")]
        public float ObjAngSpd_X {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        [Description("deg/sec")]
        public float ObjAngSpd_Y {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        [Description("deg/sec")]
        public float ObjAngSpd_Z {
            get => ReadFloat(24);
            set => Write(24, value);
        }
    }
    public enum RollCircleModel {
        Big,
        Small,
        Head
    }
}
