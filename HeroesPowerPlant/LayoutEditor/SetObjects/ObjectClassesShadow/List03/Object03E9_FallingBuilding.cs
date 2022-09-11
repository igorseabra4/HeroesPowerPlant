namespace HeroesPowerPlant.LayoutEditor
{
    public class Object03E9_FallingBuilding : SetObjectShadow
    {
        //FallingBuildingHolder obj

        public enum EObjectType : int
        {
            Bridge15Angle = 0,
            Bridge45Angle = 1,
            Building = 2,
        }

        [MiscSetting]
        public EObjectType ObjectType { get; set; }
    }
}

