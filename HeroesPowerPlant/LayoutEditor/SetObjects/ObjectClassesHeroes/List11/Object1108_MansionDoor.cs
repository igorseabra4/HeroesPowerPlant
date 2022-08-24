namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1108_MansionDoor : SetObjectHeroes
    {
        public enum EOpenAngle : int
        {
            Angle90 = 0,
            Angle180 = 1,
            Angle83dot5 = 2
        }

        [MiscSetting]
        public EOpenAngle OpenAngle { get; set; }
    }
}
