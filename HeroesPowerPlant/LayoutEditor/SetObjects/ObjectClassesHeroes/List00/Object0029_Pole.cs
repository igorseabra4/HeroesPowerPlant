using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0029_Pole : SetObjectManagerHeroes
    {
        public Int16 Length
        {
            get { return ReadWriteWord(4); }
            set { ReadWriteWord(4, value); }
        }

        public Int16 Range
        {
            get { return ReadWriteWord(6); }
            set { ReadWriteWord(6, value); }
        }

        public Int16 Start
        {
            get { return ReadWriteWord(8); }
            set { ReadWriteWord(8, value); }
        }

        public Int16 End
        {
            get { return ReadWriteWord(10); }
            set { ReadWriteWord(10, value); }
        }

        public float ReleaseElevation
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public float ReleaseAzimuth
        {
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }

        public float ReleasePower
        {
            get { return ReadWriteSingle(20); }
            set { ReadWriteSingle(20, value); }
        }

        public byte NoReference
        {
            get { return ReadWriteByte(24); }
            set { ReadWriteByte(24, value); }
        }

        public byte ReferenceID
        {
            get { return ReadWriteByte(25); }
            set { ReadWriteByte(25, value); }
        }

        public Int16 NoControlTime
        {
            get { return ReadWriteWord(26); }
            set { ReadWriteWord(26, value); }
        }
    }
}