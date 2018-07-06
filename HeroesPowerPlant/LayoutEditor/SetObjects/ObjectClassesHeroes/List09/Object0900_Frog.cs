using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0900_Frog : SetObjectManagerHeroes
    {
        public float JumpDirX
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float JumpDirY
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float JumpDirZ
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public float Radius
        {
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }

        public float Scale
        {
            get { return ReadWriteSingle(20); }
            set { ReadWriteSingle(20, value); }
        }

        public float JumpCycle
        {
            get { return ReadWriteSingle(24); }
            set { ReadWriteSingle(24, value); }
        }

        public Int16 StopTimeSec
        {
            get { return ReadWriteWord(28); }
            set { ReadWriteWord(28, value); }
        }

        public Int16 LeaveTimeSec
        {
            get { return ReadWriteWord(30); }
            set { ReadWriteWord(30, value); }
        }

        public byte FrogType
        {
            get { return ReadWriteByte(32); }
            set { ReadWriteByte(32, value); }
        }
    }
}