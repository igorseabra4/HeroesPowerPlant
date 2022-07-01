namespace HeroesPowerPlant.LayoutEditor
{
    public class Object189E_ARKDriftingPlat1 : SetObjectShadow
    {
        //FootingBreak(Type{Burst,Hover}, WaitSec, HoverLength point, HoverSec, MoveLengthX point, MoveLengthY point, MoveLengthZ point, MoveSec)
        public ARKDriftingPlatformType PlatformType
        {
            get => (ARKDriftingPlatformType)ReadInt(0x0);
            set => Write(0x0, (int)value);
        }

        public float ExplosionDelay
        {
            get => ReadFloat(0x4);
            set => Write(0x4, value);
        }

        public float HoverLength
        {
            get => ReadFloat(0x8);
            set => Write(0x8, value);
        }

        public float HoverSec
        {
            get => ReadFloat(0xC);
            set => Write(0xC, value);
        }

        public float TranslationX
        {
            get => ReadFloat(0x10);
            set => Write(0x10, value);
        }

        public float TranslationY
        {
            get => ReadFloat(0x14);
            set => Write(0x14, value);
        }

        public float TranslationZ
        {
            get => ReadFloat(0x18);
            set => Write(0x18, value);
        }
        public float TravelTime
        {
            get => ReadFloat(0x1C);
            set => Write(0x1C, value);
        }
    }

    public enum ARKDriftingPlatformType
    {
        Burst,
        Hover
    }
}
