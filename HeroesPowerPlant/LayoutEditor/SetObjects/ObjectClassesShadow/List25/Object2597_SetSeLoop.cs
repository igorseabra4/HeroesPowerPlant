using Shadow.Structures;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object2597_SetSeLoop : SetObjectShadow
    {

        public int AudioID
        {
            //30017, 29788, 30257, 30542, 30528
            get => ReadInt(0);
            set => Write(0, value);
        }

        public SFXEntry sfxEntry;

        public string AudioID_String
        {
            get
            {
                return sfxEntry.sfxString;
            }
        }

        public int RangeTypeMaybeRaw
        {
            get => ReadInt(4);
            set => Write(4, value);
        }

        public SetSeLoopRange RangeTypeMaybe
        {
            get => (SetSeLoopRange)ReadInt(4);
            set => Write(4, (int)value);
        }

        public int TypeMaybeRaw
        {
            get => ReadInt(8);
            set => Write(8, value);
        }

        public SetSeLoopType TypeMaybe
        {
            get => (SetSeLoopType)ReadInt(8);
            set => Write(8, (int)value);
        }

        public float HalfLengthX
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float HalfLengthY
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float HalfLengthZ
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public float FlatDampingRate
        { // FlatDampingRate(0-1)
            get => ReadFloat(24);
            set => Write(24, value);
        }
    }

    public enum SetSeLoopType
    {
        FlatDamping,
        PanCtrl
    }

    public enum SetSeLoopRange
    {
        Sphere,
        Box,
        Cylinder //potentially offset by Capsule if shared with SetSeOneShot
    }
}

