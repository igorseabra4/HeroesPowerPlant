namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_B1_1_Type : SetObjectManagerHeroes
    {
        public byte Type
        {
            get { return ReadWriteByte(4); }
            set { ReadWriteByte(4, value); }
        }
    }
}