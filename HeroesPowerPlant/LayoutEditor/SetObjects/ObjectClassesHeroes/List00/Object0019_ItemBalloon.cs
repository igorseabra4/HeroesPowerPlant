namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0019_ItemBalloon : SetObjectManagerHeroes
    {
            public ItemType Item
            {
                get { return (ItemType)ReadWriteByte(4); }
                set { byte a = (byte)value; ReadWriteByte(4, a); }
            }

            public float Scale
            {
                get { return ReadWriteSingle(8); }
                set { ReadWriteSingle(8, value); }
            }
        }
    }