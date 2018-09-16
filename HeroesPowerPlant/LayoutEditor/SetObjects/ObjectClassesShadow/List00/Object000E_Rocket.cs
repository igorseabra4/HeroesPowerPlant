namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000E_Rocket : SetObjectManagerShadow
    {
        public float TravelAngle
        {
            get { return ReadFloat(0); }
            set { Write(0, value); }
        }

        public float TravelDistance
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }
    }
}
