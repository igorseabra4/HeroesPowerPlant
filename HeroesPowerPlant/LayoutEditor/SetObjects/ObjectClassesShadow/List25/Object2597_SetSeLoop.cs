using Shadow.Structures;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object2597_SetSeLoop : SetObjectShadow
    {
        public enum EType : int
        {
            FlatDamping,
            PanCtrl
        }

        public enum ERange : int
        {
            Sphere,
            Box,
            Cylinder //potentially offset by Capsule if shared with SetSeOneShot
        }

        [MiscSetting(0)]
        public int AudioID { get; set; } //30017, 29788, 30257, 30542, 30528

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

        [MiscSetting(6)]
        public float FlatDampingRate { get; set; } // FlatDampingRate(0-1)
    }
}

