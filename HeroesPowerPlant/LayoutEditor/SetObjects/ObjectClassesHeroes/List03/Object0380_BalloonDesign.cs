namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0380_BalloonDesign : SetObjectHeroes
    {
        public byte BalloonType
        {
            get => ReadByte(4);
            set => Write(4, value);
        }

        public int SpdRad
        {
            get => ReadInt(8);
            set => Write(8, value);
        }

        public float Radius
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float Scale
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }
    }
}