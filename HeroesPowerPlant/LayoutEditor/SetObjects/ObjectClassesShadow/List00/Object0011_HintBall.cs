namespace HeroesPowerPlant.LayoutEditor {
    public class Object0011_HintBall : SetObjectShadow {
        //AKA SetHintRing

        public int AudioBranchID {
            get => ReadInt(0);
            set => Write(0, value);
        }

        public AudioBranchType AudioBranchType {
            get => (AudioBranchType)ReadInt(4);
            set => Write(4, (int)value);
        }

        public float Float_02 {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Float_03 {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public int Int_04 {
            get => ReadInt(16);
            set => Write(16, value);
        }
    }
}
