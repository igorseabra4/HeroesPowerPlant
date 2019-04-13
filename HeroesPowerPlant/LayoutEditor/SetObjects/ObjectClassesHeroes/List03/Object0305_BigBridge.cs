namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0305_BigBridge : SetObjectManagerHeroes
    {
        public float ColScaleX
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float ColScaleY
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float ColScaleZ
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float AnimSpeed
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }
    }
}