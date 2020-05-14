namespace HeroesPowerPlant.LayoutEditor {
    public class Object1770_GUNCamera : SetObjectShadow {
        //SetWatchCamera(posX, posY, posZ, angX, angY, angZ, width, height, depth, lookPosX, lookPosY, lookPosZ, lookAngX, lookAngY, lookAngZ)
        public float PosX {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float PosY {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float PosZ {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float AngX {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float AngY {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float AngZ {
            get => ReadFloat(20);
            set => Write(20, value);
        }
        public float Detect_X {
            get => ReadFloat(24);
            set => Write(24, value);
        }
        public float Detect_Y {
            get => ReadFloat(28);
            set => Write(28, value);
        }
        public float Detect_Z {
            get => ReadFloat(32);
            set => Write(32, value);
        }
        public float LookPosX {
            get => ReadFloat(36);
            set => Write(36, value);
        }
        public float LookPosY {
            get => ReadFloat(40);
            set => Write(40, value);
        }
        public float LookPosZ {
            get => ReadFloat(44);
            set => Write(44, value);
        }

        public float LookAngX {
            get => ReadFloat(48);
            set => Write(48, value);
        }

        public float LookAngY {
            get => ReadFloat(52);
            set => Write(52, value);
        }

        public float LookAngZ {
            get => ReadFloat(56);
            set => Write(56, value);
        }
    }
}
