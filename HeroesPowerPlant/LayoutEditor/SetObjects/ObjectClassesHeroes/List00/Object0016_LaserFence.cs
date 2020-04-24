namespace HeroesPowerPlant.LayoutEditor
{
    public enum LaserFenceType
    {
        Fixed = 0,
        Intermittent = 1,
        Switch = 2,
        Scan = 3,
        Enemy = 4
    }

    public class Object0016_LaserFence : SetObjectHeroes
    {
        public LaserFenceType LaserFenceType
        {
            get => (LaserFenceType)ReadInt(4);
            set => Write(4, (int)value);
        }

        public float Lenght
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Width
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public int Interval
        {
            get => ReadInt(16);
            set => Write(16, value);
        }

        public int SwitchID
        {
            get => ReadInt(16);
            set => Write(16, value);
        }

        public int Speed
        {
            get => ReadInt(16);
            set => Write(16, value);
        }

        public int EnemyID
        {
            get => ReadInt(16);
            set => Write(16, value);
        }
    }
}