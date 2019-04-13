namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0705_Capsule : SetObjectManagerHeroes
    {
        public override void Draw(SharpRenderer renderer, string[] modelNames, bool isSelected)
        {
            if (Type == 0)
            {
                Draw(renderer, modelNames[0], isSelected);
                Draw(renderer, modelNames[1], isSelected);
            }
            else
            {
                Draw(renderer, modelNames[2], isSelected);
                Draw(renderer, modelNames[3], isSelected);
            }
        }

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