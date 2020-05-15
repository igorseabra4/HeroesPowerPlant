namespace HeroesPowerPlant.LayoutEditor {
    public class Object1138_JumpPanel : SetObjectShadow {
        //Catapult(model, level, nocontrol time (sec))
        public int Model {
            get => ReadInt(0);
            set => Write(0, value);
        }
        public float Strength {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float NoControlTimeAfterLaunch {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float AngleX {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float Unused_AngleY {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float AngleZ {
            get => ReadFloat(20);
            set => Write(20, value);
        }
    }
}
