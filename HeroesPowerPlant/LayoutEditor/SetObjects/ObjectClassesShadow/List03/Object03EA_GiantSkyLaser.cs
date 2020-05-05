namespace HeroesPowerPlant.LayoutEditor {
    public class Object03EA_GiantSkyLaser : SetObjectShadow {
        public CommonYesNo HurtPlayer {
            get => (CommonYesNo)ReadInt(0);
            set => Write(0, (int)value);
        }

        public float DetectRadius {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float Delay {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}

