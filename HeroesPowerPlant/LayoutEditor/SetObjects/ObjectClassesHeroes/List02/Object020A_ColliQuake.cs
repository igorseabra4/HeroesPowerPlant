namespace HeroesPowerPlant.LayoutEditor
{
    public class Object020A_ColliQuake : SetObjectManagerHeroes
    {
        public float ScaleX
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float ScaleY
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float ScaleZ
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public float Strenght
        {
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }

        public int Time
        {
            get { return ReadLong(20); }
            set { Write(20, value); }
        }
    }
}
