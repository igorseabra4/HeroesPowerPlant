namespace HeroesPowerPlant.LayoutEditor
{
    public class Object090E_Alligator : SetObjectManagerHeroes
    {
        public float StartPosX
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float UnknownFloat1
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float UnknownFloat2
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
        public float UnknownFloat3
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }
        public float UnknownFloat4
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }
    }
}