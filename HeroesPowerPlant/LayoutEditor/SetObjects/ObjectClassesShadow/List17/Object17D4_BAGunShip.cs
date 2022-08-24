using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object11D4_BAGunShip : SetObjectShadow
    {
        public enum EBAGunShipAttack : int
        {
            NONE,
            ATTACK,
            ATTACK2 = 5
        }

        //radius, height, attack, ComID, delay, interval

        [MiscSetting]
        public float DetectRangeXZ { get; set; } //radius
        [MiscSetting]
        public float DetectRangeY { get; set; } //height
        [MiscSetting]
        public float TimeUntilTakeOff { get; set; } //delay

        [MiscSetting]
        public EBAGunShipAttack Attack { get; set; } //attack

        [MiscSetting, Description("Always 0 in original objects, purpose unknown")]
        public int int4 { get; set; }

        [MiscSetting, Description("Enemy w/LinkID to spawn")]
        public int LinkID_EnemySpawning { get; set; } //ComID

        [MiscSetting, Description("Number of times to re-drop enemy from LinkID_EnemySpawning")]
        public int NumberOfDrops { get; set; }

        [MiscSetting, Description("Always 0 in original objects, purpose unknown")]
        public int int7 { get; set; }

        [MiscSetting]
        public float AttackInterval { get; set; } //interval
    }
}

