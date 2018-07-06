namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0015_SpikeBall : SetObjectManagerHeroes
    {
        public enum SpikeBallType
        {
            SingleBall = 0,
            DoubleBall = 1
        }

        public SpikeBallType Type
        {
            get { return (SpikeBallType)ReadWriteLong(4); }
            set { ReadWriteLong(4, (int)value); }
        }

        public float RotateSpeed
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float Scale
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }
    }
}