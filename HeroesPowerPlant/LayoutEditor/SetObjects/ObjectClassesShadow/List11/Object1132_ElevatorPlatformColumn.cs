namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1132_ElevatorPlatformColumn : SetObjectShadow
    {
        //Elevator(model, distance, speed, range)
        [MiscSetting]
        public int Model { get; set; }
        [MiscSetting]
        public float TravelDistance { get; set; }
        [MiscSetting]
        public float Speed { get; set; }
        [MiscSetting]
        public float DetectRange { get; set; }
    }
}
