namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0018_ItemBox : SetObjectManagerHeroes
    {
        public Item Item
        {
            get { return (Item)ReadByte(4); }
            set { Write(4, (byte)value); }
        }

        public bool HomingOff
        {
            get { return ReadByte(5) != 0; }
            set { Write(5, (byte)(value ? 1 : 0)); }
        }
    }
}