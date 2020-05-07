namespace HeroesPowerPlant.LayoutEditor {
    public class Object07D1_Searchlight : SetObjectShadow {
        // ElecSearchLight(ENEMY_ID, RotateRange, RotateSpeed, LightLength)
        public int SpotOnLinkID { //0 = No or 1 = Yes
            get => ReadInt(0);
            set => Write(0, value);
        }

        public float RotateRange {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float RotateSpeed {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float LightLength {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}

