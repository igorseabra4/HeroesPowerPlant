namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1392_SpaceDebris : SetObjectShadow
    {
        public enum EModel : int
        {
            Model0,
            Model1,
            Model2,
            Model3,
            Model4,
            Model5
        }

        //SpaceGadget(RotDegSpdX,RotDegSpdY,RotDegSpdZ)
        [MiscSetting]
        public EModel Model { get; set; }
        [MiscSetting]
        public float RotDegSpdX { get; set; }
        [MiscSetting]
        public float RotDegSpdY { get; set; }
        [MiscSetting]
        public float RotDegSpdZ { get; set; }
    }
}
