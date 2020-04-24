using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object090B_IvyJump : SetObjectHeroes
    {
        public float Target1X
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float Target1Y
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Target1Z
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float Target2X
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float Target2Y
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public float Target2Z
        {
            get => ReadFloat(24);
            set => Write(24, value);
        }

        [DisplayName("KazariType(?)")]
        public short KazariType
        {
            get => ReadByte(28);
            set => Write(28, value);
        }

        [DisplayName("Dammy(?)")]
        public short Dammy
        {
            get => ReadByte(29);
            set => Write(29, value);
        }

        public short NoControlTime
        {
            get => ReadShort(30);
            set => Write(30, value);
        }
    }
}