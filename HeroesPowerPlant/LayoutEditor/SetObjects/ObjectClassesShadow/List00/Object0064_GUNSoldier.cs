namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0064_GUNSoldier : SetObjectShadow
    {
        public enum EAppear : int
        {
            STAND,
            LINEAR_MOVE,
            TRIANGLE_MOVE,
            RANDOM_MOVE,
            OFFSETPOS,
            HINSHI
        }

        public enum EWeapon : int
        {
            NONE,
            KNIFE,
            GUN,
            MACHINEGUN,
            RIFLE,
            GRENADE,
            MISSILE
        }

        public enum EWaitType : int
        {
            RADIO_CONTACT,
            ATTACK,
            HIDE,
            KAMAE
        }

        // EnemyBase
        [MiscSetting(0)]
        public float MoveRange { get; set; }
        [MiscSetting(1)]
        public float SearchRange { get; set; }
        [MiscSetting(2)]
        public float SearchAngle { get; set; }
        [MiscSetting(3)]
        public float SearchWidth { get; set; }
        [MiscSetting(4)]
        public float SearchHeight { get; set; }
        [MiscSetting(5)]
        public float SearchHeightOffset { get; set; }
        [MiscSetting(6)]
        public float MoveSpeedRatio { get; set; }

        // end EnemyBase

        [MiscSetting(7)]
        public ENoYes HaveShield { get; set; }
        [MiscSetting(8)]
        public EWeapon WeaponType { get; set; }
        [MiscSetting(9)]
        public EAppear AppearType { get; set; }
        [MiscSetting(10)]
        public EWaitType Pos0_WaitType { get; set; }
        [MiscSetting(11)]
        public float Pos0_WaitSec { get; set; }
        [MiscSetting(12)]
        public float Pos0_MoveSpeedRatio { get; set; }
        [MiscSetting(13)]
        public float Pos0_TranslationXFromOrigin { get; set; }
        [MiscSetting(14)]
        public float Pos0_TranslationZFromOrigin { get; set; }
        [MiscSetting(15)]
        public EWaitType Pos1_WaitType { get; set; }
        [MiscSetting(16)]
        public float Pos1_WaitSec { get; set; }
        [MiscSetting(17)]
        public float Pos1_MoveSpeedRatio { get; set; }
        [MiscSetting(18)]
        public float Pos1_TranslationXFromOrigin { get; set; }
        [MiscSetting(19)]
        public float Pos1_TranslationZFromOrigin { get; set; }
        [MiscSetting(20)]
        public EWaitType Pos2_WaitType { get; set; }
        [MiscSetting(21)]
        public float Pos2_WaitSec { get; set; }
        [MiscSetting(22)]
        public float Pos2_MoveSpeedRatio { get; set; }
    }
}
