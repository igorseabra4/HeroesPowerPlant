namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1101_CastleDoor : SetObjectManagerHeroes
    {
        public bool IsUpsideDown
        {
            get => ReadLong(4) != 0;
            set => Write(4, value ? 1 : 0);
        }
    }
}
