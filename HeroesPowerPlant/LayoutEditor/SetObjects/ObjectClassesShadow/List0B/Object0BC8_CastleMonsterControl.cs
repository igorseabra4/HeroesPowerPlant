namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0BC8_CastleMonsterControl : SetObjectShadow
    {
        public enum EState : int
        {
            Start,
            Miss,
            End
        }

        //MonsterControl
        [MiscSetting]
        public EState State { get; set; }
        [MiscSetting]
        public float X_Length { get; set; }
        [MiscSetting]
        public float Y_Length { get; set; }
        [MiscSetting]
        public float Z_Length { get; set; }
    }

}
