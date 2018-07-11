namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0018_ItemBox : SetObjectManagerHeroes
    {
        public ItemType Item
        {
            get { return (ItemType)ReadByte(4); }
            set { byte a = (byte)value; Write(4, a); }
        }

        public bool HomingOff
        {
            get { return ReadByte(5) != 0; }
            set { Write(5, value ? (byte)1 : (byte)0); }
        }
    }
}