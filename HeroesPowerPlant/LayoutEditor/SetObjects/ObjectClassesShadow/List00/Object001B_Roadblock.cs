namespace HeroesPowerPlant.LayoutEditor
{
    public class Object001B_Roadblock : SetObjectShadow
    {
        public enum ERoadblockType : int
        {
            GUN,
            BlackArms
        }

        [MiscSetting]
        public ERoadblockType RoadblockType { get; set; }
    }
}

