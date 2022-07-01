using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07D5_LightspeedRisingBlock : SetObjectShadow
    {
        // ElecBlock
        // Enums: BlockType 0-9; CollisionFlag<FALSE/TRUE>
        // ElecBlock(BlockType, SearchRange, InitialY, CollisionFlag)

        public ElecBlockType BlockType
        {
            get => (ElecBlockType)ReadInt(0);
            set => Write(0, (int)value);
        }
        public float Scale_X
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float Scale_Y
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float Scale_Z
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float SearchRange
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        [Description("Move object starting point/appearance on the Y axis this amount.\n Animates to original position on detect")]
        public float Initial_Y
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        [Description("Note: Might only work with BlockType6")]
        public CommonNoYes CollisionOn
        {
            get => (CommonNoYes)ReadInt(24);
            set => Write(24, (int)value);
        }
    }

    public enum ElecBlockType
    {
        Type0,
        Type1,
        Type2,
        Type3,
        Type4,
        Type5,
        Type6,
        Type7,
        Type8,
        Type9
    }
}
