namespace HeroesPowerPlant.LayoutEditor {
    public class Object0008_Pulley : SetObjectShadow {
        
        // Real name UpDownReel / UD_REEL::length,angle,power

        public float StartingLength {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float EndingLength {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float LetGoAngle {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float LetGoLaunchForce {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}

