namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1839_RisingLava : SetObjectShadow
    {
        //SetMagma
        public RisingLava_Model Model
        {
            get => (RisingLava_Model)ReadInt(0);
            set => Write(0, (int)value);
        }
        public float RiseAmountMax
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float RiseAmountPerSecond
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }

    public enum RisingLava_Model
    {
        Model_1,
        Model_2,
        Model_3,
        Model_4,
        Model_5,
        Model_6,
        Model_7,
        Model_8,
        Model_9
    }
}
