namespace HeroesPowerPlant.LayoutEditor {
    public class Object13EF_SecurityLaser : SetObjectShadow {
        //SetSecurityLaser(_t, model, length, rot time(sec), rot Y(deg), move sec, type)
        //enum: Normal, Fixed, Fixed w/Eff
        public int Model {
            get => ReadInt(0);
            set => Write(0, value);
        }
        public float Length {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float RotTime {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float Rot_Y {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float MoveSec {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float Translate_X {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public float Translate_Y {
            get => ReadFloat(24);
            set => Write(24, value);
        }

        public float Translate_Z {
            get => ReadFloat(28);
            set => Write(28, value);
        }

        public SecurityLaserType LaserType {
            get => (SecurityLaserType)ReadInt(32);
            set => Write(32, (int)value);
        }
    }

    public enum SecurityLaserType {
        DoNotGoThroughObjectsOnCollide_With_CollideEffect, //Normal
        GoThroughObjects_Without_CollideEffect, //Fixed
        GoThroughObjects_With_CollideEffect //FixedWEffect
    }
}
