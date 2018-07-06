namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0025_FormSign : SetObjectManagerHeroes
    {
        public Formations Formation
        {
            get { return (Formations)ReadWriteByte(4); }
            set { byte a = (byte)value; ReadWriteByte(4, a); }
        }
    }
}