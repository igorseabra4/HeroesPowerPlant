namespace HeroesPowerPlant.LayoutEditor
{
    public class Object002C_RollDoor : SetObjectManagerHeroes
    {
        public float Power
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float Elevation
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public short NoControlTime
        {
            get { return ReadShort(12); }
            set { Write(12, value); }
        }
    }
}