namespace HeroesPowerPlant.LayoutEditor
{
    public class Object002C_RollDoor : SetObjectHeroes
    {
        public float Power
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float Elevation
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public short NoControlTime
        {
            get => ReadShort(12);
            set => Write(12, value);
        }
    }
}