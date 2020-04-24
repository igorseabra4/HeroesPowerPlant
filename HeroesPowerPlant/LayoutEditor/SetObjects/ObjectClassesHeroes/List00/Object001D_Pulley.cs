namespace HeroesPowerPlant.LayoutEditor
{
    public class Object001D_Pulley : SetObjectHeroes
    {
        public float Elevation
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float ElevationAngle
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Power
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public PulleyType PulleyType
        {
            get => (PulleyType)ReadShort(16);
            set => Write(16, (short)value);
        }
    }

    public enum PulleyType : short
    {
        Up = 0,
        Down = 1
    }
}