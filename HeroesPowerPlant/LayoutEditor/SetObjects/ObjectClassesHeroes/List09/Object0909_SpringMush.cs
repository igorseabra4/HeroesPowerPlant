namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0909_SpringMush : SetObjectHeroes
    {
        public float Speed
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public byte NoControlTime
        {
            get => ReadByte(8);
            set => Write(8, value);
        }
    }
}