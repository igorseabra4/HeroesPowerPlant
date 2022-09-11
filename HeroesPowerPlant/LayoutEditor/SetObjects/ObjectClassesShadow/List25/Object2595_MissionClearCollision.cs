namespace HeroesPowerPlant.LayoutEditor
{
    public class Object2595_MissionClearCollision : SetObjectShadow
    {
        public enum EMissionType : int
        {
            Dark,
            Normal,
            Hero
        }

        [MiscSetting]
        public EMissionType MissionType { get; set; }

        [MiscSetting]
        public float Radius { get; set; }
    }
}

