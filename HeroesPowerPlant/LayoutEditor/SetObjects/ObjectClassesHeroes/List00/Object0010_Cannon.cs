namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0010_Cannon : SetObjectManagerHeroes
    {
        public short SpeedElevation
        {
            get => ReadShort(4);
            set => Write(4, value);
        }

        public short SpeedAzimuth
        {
            get => ReadShort(6);
            set => Write(6, value);
        }

        public short SpeedNoControlTime
        {
            get => ReadShort(8);
            set => Write(8, value);
        }

        public short SpeedPower
        {
            get => ReadShort(10);
            set => Write(10, value);
        }

        public short FlyElevation
        {
            get => ReadShort(12);
            set => Write(12, value);
        }

        public short FlyAzimuth
        {
            get => ReadShort(14);
            set => Write(14, value);
        }

        public short FlyNoControlTime
        {
            get => ReadShort(16);
            set => Write(16, value);
        }

        public short FlyPower
        {
            get => ReadShort(18);
            set => Write(18, value);
        }

        public short PowerElevation
        {
            get => ReadShort(20);
            set => Write(20, value);
        }

        public short PowerAzimuth
        {
            get => ReadShort(22);
            set => Write(22, value);
        }

        public short PowerNoControlTime
        {
            get => ReadShort(24);
            set => Write(24, value);
        }

        public short PowerPower
        {
            get => ReadShort(26);
            set => Write(26, value);
        }
    }
}