namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0032_WarpFlower : SetObjectManagerHeroes
    {
        public enum FlowerType : byte
        {
            Item = 0,
            Scaffold = 1,
            Warp = 2
        }

        public FlowerType Type
        {
            get { return (FlowerType)ReadWriteByte(4); }
            set { byte a = (byte)value; ReadWriteByte(4, a); }
        }

        public float Scale
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float RisingHeight
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }
    }
}