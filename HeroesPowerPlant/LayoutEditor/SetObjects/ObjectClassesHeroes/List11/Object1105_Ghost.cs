namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1105_Ghost : SetObjectManagerHeroes
    {
        public enum GhostType
        {
            NoMove = 0,
            Line = 1,
            Circle = 2
        }

        public GhostType Type
        {
            get { return (GhostType)ReadLong(4); }
            set { Write(4, (int)value); }
        }

        public float Range
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float MovingArea
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public float Speed
        {
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }

        public float Scale
        {
            get { return ReadFloat(20); }
            set { Write(20, value); }
        }
    }
}
