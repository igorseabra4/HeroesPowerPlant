namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000F_Platform : SetObjectShadow
    {
        // Needs further research
        // ElecBlock
        // Enums: BlockType 0-9 (unused?); CollisionFlag<FALSE/TRUE>
        // ElecBlock(BlockType, SearchRange, InitialY, CollisionFlag)
        public PlatformBlockType PlatformType
        {
            get => (PlatformBlockType)ReadInt(0);
            set => Write(0, (int)value);
        }

        public PlatformMoveType MovementType
        {
            get => (PlatformMoveType)ReadInt(4);
            set => Write(4, (int)value);
        }

        public float TravelTime
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float WaitTime
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float float_10
        {
            get => ReadFloat(0x10);
            set => Write(0x10, value);
        }

        public float float_14
        {
            get => ReadFloat(0x14);
            set => Write(0x14, value);
        }

        public float TranslationX
        {
            get => ReadFloat(0x18);
            set => Write(0x18, value);
        }

        public float TranslationY
        {
            get => ReadFloat(0x1C);
            set => Write(0x1C, value);
        }

        public float TranslationZ
        {
            get => ReadFloat(0x20);
            set => Write(0x20, value);
        }

        public float float_24
        {
            get => ReadFloat(0x24);
            set => Write(0x24, value);
        }
    }

    public enum PlatformBlockType {
        Type0,
        Type1,
        Type2,
        Type3
    }

    public enum PlatformMoveType {
        Linear=0,
        Unknown2=2,
        TranslationOnLinkID=3,
        Lerp=4,
        Slerp=6
    }
}
