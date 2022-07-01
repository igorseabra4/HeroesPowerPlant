using Shadow.Structures;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object2598_SetSeOneShot : SetObjectShadow
    {

        public int AudioID
        { //28980, 29004
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

        public SetSeOneShotRange RangeTypeMaybe
        {
            get => (SetSeOneShotRange)ReadInt(4);
            set => Write(4, (int)value);
        }

        public int TypeMaybeRaw
        {
            get => ReadInt(8);
            set => Write(8, value);
        }

        public SetSeOneShotType TypeMaybe
        {
            get => (SetSeOneShotType)ReadInt(8);
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
    }

    public enum SetSeOneShotType
    {
        OnlyOnce,
        CallAgain
    }

    public enum SetSeOneShotRange
    {
        Sphere,
        Box,
        Capsule,
        Cylinder
    }
}

