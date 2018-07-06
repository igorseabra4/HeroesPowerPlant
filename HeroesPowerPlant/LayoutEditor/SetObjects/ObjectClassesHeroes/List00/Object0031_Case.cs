namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0031_Case : SetObjectManagerHeroes
    {
        public float ScaleX
        {
            get { return ReadWriteSingle(4); }
            set
            {
                ReadWriteSingle(4, value);
            }
        }

        public float ScaleY
        {
            get { return ReadWriteSingle(8); }
            set
            {
                ReadWriteSingle(8, value);
            }
        }

        public float ScaleZ
        {
            get { return ReadWriteSingle(12); }
            set
            {
                ReadWriteSingle(12, value);
            }
        }

        public byte LinkID
        {
            get { return ReadWriteByte(16); }
            set { ReadWriteByte(16, value); }
        }

        public enum DirectionType : byte
        {
            Up = 0,
            Down = 1,
        }

        public DirectionType Direction
        {
            get { return (DirectionType)ReadWriteByte(17); }
            set { ReadWriteByte(17, (byte)value); }
        }
    }
}