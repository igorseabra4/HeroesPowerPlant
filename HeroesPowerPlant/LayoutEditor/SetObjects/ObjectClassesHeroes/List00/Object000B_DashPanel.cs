namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000B_DashPanel : SetObjectManagerHeroes
    {
        public float Speed
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public short ControlTime
        {
            get => ReadShort(8);
            set => Write(8, value);
        }
    }
}