using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0010_Cannon : SetObjectManagerHeroes
    {
        public Int16 SpeedElevation
        {
            get { return ReadShort(4); }
            set { Write(4, value); }
        }

        public Int16 SpeedAzimuth
        {
            get { return ReadShort(6); }
            set { Write(6, value); }
        }

        public Int16 SpeedNoControlTime
        {
            get { return ReadShort(8); }
            set { Write(8, value); }
        }

        public Int16 SpeedPower
        {
            get { return ReadShort(10); }
            set { Write(10, value); }
        }

        public Int16 FlyElevation
        {
            get { return ReadShort(12); }
            set { Write(12, value); }
        }

        public Int16 FlyAzimuth
        {
            get { return ReadShort(14); }
            set { Write(14, value); }
        }

        public Int16 FlyNoControlTime
        {
            get { return ReadShort(16); }
            set { Write(16, value); }
        }

        public Int16 FlyPower
        {
            get { return ReadShort(18); }
            set { Write(18, value); }
        }

        public Int16 PowerElevation
        {
            get { return ReadShort(20); }
            set { Write(20, value); }
        }

        public Int16 PowerAzimuth
        {
            get { return ReadShort(22); }
            set { Write(22, value); }
        }

        public Int16 PowerNoControlTime
        {
            get { return ReadShort(24); }
            set { Write(24, value); }
        }

        public Int16 PowerPower
        {
            get { return ReadShort(26); }
            set { Write(26, value); }
        }
    }
}