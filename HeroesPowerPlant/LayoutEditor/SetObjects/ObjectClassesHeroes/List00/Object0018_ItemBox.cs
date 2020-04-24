namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0018_ItemBox : SetObjectHeroes
    {
        public Item Item
        {
            get => (Item)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public bool HomingOff
        {
            get => ReadByte(5) != 0;
            set => Write(5, (byte)(value ? 1 : 0));
        }
    }
}