namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0004_DashRamp : SetObjectShadow
    {
        public float Strength
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }

        public float Height
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float NoControlTime
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}
