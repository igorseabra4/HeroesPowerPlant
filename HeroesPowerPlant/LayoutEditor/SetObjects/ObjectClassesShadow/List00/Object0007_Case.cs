namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0007_Case : SetObjectShadow
    {
        public LockedCaseType CaseType
        {
            get => (LockedCaseType)ReadInt(0);
            set => Write(0, (int)value);
        }

        public float ScaleX
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float ScaleY
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float ScaleZ
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }

    public enum LockedCaseType {
        BlackArms,
        GUN
    }
}

