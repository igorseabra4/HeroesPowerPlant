namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0004_DashRamp : SetObjectManagerShadow
    {
        public float Strenght
        {
            get { return ReadFloat(0); }
            set { Write(0, value); }
        }

        public float Height
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float ControlTime
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }
    }
}
