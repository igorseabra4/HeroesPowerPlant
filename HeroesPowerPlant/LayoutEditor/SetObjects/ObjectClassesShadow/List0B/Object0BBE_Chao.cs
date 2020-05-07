namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0BBE_Chao : SetObjectShadow {
        //ChaoHolder
        public Chao ChaoType
        {
            get => (Chao)ReadInt(0);
            set => Write(0, (int)value);
        }

        public float MoveRadius
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float MoveSpeed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }

    public enum Chao
    {
        Normal = 0x00, //CHAO
        Cheese = 0x01 //CHEEZ
    }
}
