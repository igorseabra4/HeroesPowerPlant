namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1108_MansionDoor : SetObjectManagerHeroes
    {
        public enum Angle
        {
            Angle90 = 0,
            Angle180 = 1,
            Angle83dot5 = 2
        }

        public Angle OpenAngle
        {
            get { return (Angle)ReadLong(4); }
            set { Write(4, (int)value); }
        }
    }
}
