using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object1451_CommandCollision : SetObjectShadow {
        //CommandCollision

        public float DetectX {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float DetectY {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float DetectZ {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("Known types (values unknown): Move, Stop, Accelerate, SetPos")]
        public int ActionID { //0, 1, 2, 3, 6, 7
            get => ReadInt(12);
            set => Write(12, value);
        }

        [Description("Known types (values unknown): no timer, route, Not in use, speed, rate(0.0-1.0), type")]
        public float ActionParam0 {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        [Description("Known types (values unknown): no timer, route, Not in use, speed, rate(0.0-1.0), type")]
        public float ActionParam1 {
            get => ReadFloat(20);
            set => Write(20, value);
        }
    }
}
