namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1303_MovingCannon : SetObjectManagerHeroes
    {
        public float MaxHeight
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
    }
}