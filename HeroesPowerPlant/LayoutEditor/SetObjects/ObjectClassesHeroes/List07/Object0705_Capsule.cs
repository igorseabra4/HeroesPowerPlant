namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0705_Capsule : SetObjectManagerHeroes
    {
        public Item Itembox
        {
            get => (Item)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public byte Type
        {
            get => ReadByte(5);
            set => Write(5, value);
        }
    }
}