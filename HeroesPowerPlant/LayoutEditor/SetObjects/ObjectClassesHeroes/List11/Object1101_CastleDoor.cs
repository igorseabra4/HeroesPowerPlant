namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1101_CastleDoor : SetObjectHeroes
    {
        public bool IsUpsideDown
        {
            get => ReadInt(4) != 0;
            set => Write(4, value ? 1 : 0);
        }
    }
}
