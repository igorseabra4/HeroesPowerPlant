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
            get => (CageType)ReadLong(4);
            set => Write(4, (int)value);
        }

        public float MoveCycleSec
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float MoveRangeH
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float MoveRangeV
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }
    }
}