namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_B1_1_Type : SetObjectManagerHeroes
    {
        public byte Type
        {
            get { return ReadByte(4); }
            set { Write(4, value); }
        }
    }
}