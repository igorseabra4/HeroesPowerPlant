using Shadow.Structures;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object2598_SetSeOneShot : SetObjectShadow
    {
        public enum EType : int
        {
            OnlyOnce,
            CallAgain
        }

        public enum ERange : int
        {
            Sphere,
            Box,
            Capsule,
            Cylinder
        }

        [MiscSetting(0)]
        public int AudioID { get; set; } //28980, 29004

        public SFXEntry sfxEntry;

        public string AudioID_String
        {
            get
            {
                return sfxEntry.sfxString;
            }
        }

        [MiscSetting(1)]
        public int RangeTypeMaybeRaw { get; set; }

        public ERange RangeTypeMaybe
        {
            get => (ERange)RangeTypeMaybeRaw;
            set => RangeTypeMaybeRaw = (int)value;
        }

        [MiscSetting(2)]
        public int TypeMaybeRaw { get; set; }

        public EType TypeMaybe
        {
            get => (EType)TypeMaybeRaw;
            set => TypeMaybeRaw = (int)value;
        }

        [MiscSetting(3)]
        public float HalfLengthX { get; set; }

        [MiscSetting(4)]
        public float HalfLengthY { get; set; }

        [MiscSetting(5)]
        public float HalfLengthZ { get; set; }
    }
}

