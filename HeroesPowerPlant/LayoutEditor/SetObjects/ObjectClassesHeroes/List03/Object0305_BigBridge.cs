namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0305_BigBridge : SetObjectManagerHeroes
    {
        public float ColScaleX
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float ColScaleY
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float ColScaleZ
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public float AnimSpeed
        {
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }
    }
}