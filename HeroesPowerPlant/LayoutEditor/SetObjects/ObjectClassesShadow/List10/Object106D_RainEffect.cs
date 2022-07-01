namespace HeroesPowerPlant.LayoutEditor
{
    public class Object106D_RainEffect : SetObjectShadow
    {
        //RainEffect(COLLI_WIDTH m, COLLI_HEIGHT m, COLLI_DEPTH m)
        public float Range_X
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float Range_Y
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float Range_Z
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}
