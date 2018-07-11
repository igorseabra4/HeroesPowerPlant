namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0025_FormSign : SetObjectManagerHeroes
    {
        public Formations Formation
        {
            get { return (Formations)ReadByte(4); }
            set { byte a = (byte)value; Write(4, a); }
        }
    }
}