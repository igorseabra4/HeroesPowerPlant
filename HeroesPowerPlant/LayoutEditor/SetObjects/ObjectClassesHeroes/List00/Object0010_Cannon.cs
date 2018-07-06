using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0010_Cannon : SetObjectManagerHeroes
    {
        public Int16 SpeedElevation
        {
            get { return ReadWriteWord(4); }
            set { ReadWriteWord(4, value); }
        }

        public Int16 SpeedAzimuth
        {
            get { return ReadWriteWord(6); }
            set { ReadWriteWord(6, value); }
        }

        public Int16 SpeedNoControlTime
        {
            get { return ReadWriteWord(8); }
            set { ReadWriteWord(8, value); }
        }

        public Int16 SpeedPower
        {
            get { return ReadWriteWord(10); }
            set { ReadWriteWord(10, value); }
        }

        public Int16 FlyElevation
        {
            get { return ReadWriteWord(12); }
            set { ReadWriteWord(12, value); }
        }

        public Int16 FlyAzimuth
        {
            get { return ReadWriteWord(14); }
            set { ReadWriteWord(14, value); }
        }

        public Int16 FlyNoControlTime
        {
            get { return ReadWriteWord(16); }
            set { ReadWriteWord(16, value); }
        }

        public Int16 FlyPower
        {
            get { return ReadWriteWord(18); }
            set { ReadWriteWord(18, value); }
        }

        public Int16 PowerElevation
        {
            get { return ReadWriteWord(20); }
            set { ReadWriteWord(20, value); }
        }

        public Int16 PowerAzimuth
        {
            get { return ReadWriteWord(22); }
            set { ReadWriteWord(22, value); }
        }

        public Int16 PowerNoControlTime
        {
            get { return ReadWriteWord(24); }
            set { ReadWriteWord(24, value); }
        }

        public Int16 PowerPower
        {
            get { return ReadWriteWord(26); }
            set { ReadWriteWord(26, value); }
        }
    }
}