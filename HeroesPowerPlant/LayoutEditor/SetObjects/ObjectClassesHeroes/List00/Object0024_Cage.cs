namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0024_Cage : SetObjectManagerHeroes
    {
        public enum CageType : byte
        {
            PFixed = 0,
            PFlying = 1,
            UFixed = 2,
            UFlying = 3
        }

        public CageType Type
        {
            get { return (CageType)ReadWriteLong(4); }
            set { ReadWriteLong(4, (int)value); }
        }

        public float MoveCycleSec
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float MoveRangeH
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public float MoveRangeV
        {
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }
    }
}