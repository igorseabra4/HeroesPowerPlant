using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object00_Weight : SetObjectManagerHeroes
    {
        public enum WeightType
        {
            Repeat = 0,
            Shadow = 1,
            Laser = 2,
            RepeatSwitch = 3,
            ShadowSwitch = 4,
            LaserSwitch = 5
        }

        public WeightType Type
        {
            get { return (WeightType)ReadWriteByte(4); }
            set { ReadWriteByte(4, (byte)value); }
        }

        public byte LinkID
        {
            get { return ReadWriteByte(5); }
            set { ReadWriteByte(5, value); }
        }

        public Int16 Height
        {
            get { return ReadWriteWord(6); }
            set { ReadWriteWord(6, value); }
        }

        public float ScaleX
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float ScaleZ
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public Int16 UpWaitTime
        {
            get { return ReadWriteWord(16); }
            set { ReadWriteWord(16, value); }
        }

        public Int16 DownWaitTime
        {
            get { return ReadWriteWord(18); }
            set { ReadWriteWord(18, value); }
        }

        public float ScaleY
        {
            get { return ReadWriteSingle(20); }
            set { ReadWriteSingle(20, value); }
        }
    }
}