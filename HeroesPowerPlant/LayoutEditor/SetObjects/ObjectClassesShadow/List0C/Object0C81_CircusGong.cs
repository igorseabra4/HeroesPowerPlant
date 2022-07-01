namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0C81_CircusGong : SetObjectShadow
    {
        //Gong(speed)
        public float Speed
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }
    }
}
