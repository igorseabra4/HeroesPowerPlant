namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000E_Checkpoint : SetObjectManagerHeroes
    {
        public short Priority
        {
            get { return ReadShort(4); }
            set { Write(4, value); }
        }
    }
}