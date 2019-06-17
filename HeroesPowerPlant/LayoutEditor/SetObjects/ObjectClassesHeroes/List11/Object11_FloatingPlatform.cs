namespace HeroesPowerPlant.LayoutEditor
{
    public enum PlatformType
    {
        Fixed = 0,
        Moving = 1,
        Alternate = 2,
        Disappear = 3
    }

    public class Object11_FloatingPlatform : SetObjectManagerHeroes
    {
        public PlatformType PlatformType
        {
            get => (PlatformType)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public bool AlternateModel
        {
            get => ReadByte(5) != 0;
            set => Write(5, (byte)(value ? 1 : 0));
        }

        public short UnknownAlternateRange0
        {
            get => ReadShort(6);
            set => Write(8, value);
        }

        public short UnknownAlternateRange1
        {
            get => ReadShort(8);
            set => Write(8, value);
        }

        public short XOffset
        {
            get => ReadShort(10);
            set => Write(10, value);
        }

        public short YOffset
        {
            get => ReadShort(12);
            set => Write(12, value);
        }

        public short ZOffset
        {
            get => ReadShort(14);
            set => Write(14, value);
        }

        public short TimeCycleFrame
        {
            get => ReadShort(16);
            set => Write(16, value);
        }

        public byte DisappearLinkID
        {
            get => ReadByte(18);
            set => Write(18, value);
        }
    }
}
