using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0900_Frog : SetObjectManagerHeroes
    {
        public float JumpDirX
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float JumpDirY
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float JumpDirZ
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public float Radius
        {
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }

        public float Scale
        {
            get { return ReadFloat(20); }
            set { Write(20, value); }
        }

        public float JumpCycle
        {
            get { return ReadFloat(24); }
            set { Write(24, value); }
        }

        public Int16 StopTimeSec
        {
            get { return ReadShort(28); }
            set { Write(28, value); }
        }

        public Int16 LeaveTimeSec
        {
            get { return ReadShort(30); }
            set { Write(30, value); }
        }

        public byte FrogType
        {
            get { return ReadByte(32); }
            set { Write(32, value); }
        }
    }
}