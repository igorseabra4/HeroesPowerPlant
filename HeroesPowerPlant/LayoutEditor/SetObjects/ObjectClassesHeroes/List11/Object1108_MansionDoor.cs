namespace HeroesPowerPlant.LayoutEditor
{
    public enum OpenAngle
    {
        Angle90 = 0,
        Angle180 = 1,
        Angle83dot5 = 2
    }

    public class Object1108_MansionDoor : SetObjectManagerHeroes
    {
        public OpenAngle OpenAngle
        {
            get => (OpenAngle)ReadInt(4);
            set => Write(4, (int)value);
        }
    }
}
