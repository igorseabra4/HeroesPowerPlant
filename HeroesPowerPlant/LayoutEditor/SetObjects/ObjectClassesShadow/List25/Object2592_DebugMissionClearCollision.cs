namespace HeroesPowerPlant.LayoutEditor
{
    public class Object2592_DebugMissionClearCollision : SetObjectShadow
    {
        public enum EMissionType
        {
            Dark,
            Normal,
            Hero,
            Boss
        }

        [MiscSetting]
        public EMissionType MissionType { get; set; }
    }

}

