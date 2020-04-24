namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0012_ItemCapsule : SetObjectShadow
    {
        public ItemShadow Item
        {
            get => (ItemShadow)ReadInt(0);
            set => Write(0, (int)value);
        }
    }
}
