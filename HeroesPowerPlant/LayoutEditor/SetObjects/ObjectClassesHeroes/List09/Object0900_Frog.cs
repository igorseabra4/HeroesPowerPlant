namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0900_Frog : SetObjectManagerHeroes
    {
        public float JumpDirX
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float JumpDirY
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float JumpDirZ
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float Radius
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float Scale
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public float JumpCycle
        {
            get => ReadFloat(24);
            set => Write(24, value);
        }

        public short StopTimeSec
        {
            get => ReadShort(28);
            set => Write(28, value);
        }

        public short LeaveTimeSec
        {
            get => ReadShort(30);
            set => Write(30, value);
        }

        public byte FrogType
        {
            get => ReadByte(32);
            set => Write(32, value);
        }
    }
}