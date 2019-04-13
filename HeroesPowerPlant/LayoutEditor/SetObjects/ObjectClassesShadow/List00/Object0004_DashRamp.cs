namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0004_DashRamp : SetObjectManagerShadow
    {
        public float Strenght
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }

        public float Height
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float ControlTime
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}
