namespace HeroesPowerPlant.LayoutEditor
{
    public class Object090E_Alligator : SetObjectHeroes
    {
        public float StartPosX
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float StartPosY
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float StartPosZ
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
        public float StartRange
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }
        public float SpeedRate
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }
    }
}