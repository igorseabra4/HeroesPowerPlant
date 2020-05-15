using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor {
    public class Object11D4_BAGunShip : SetObjectShadow {
        //radius, height, attack, ComID, delay, interval

        public float DetectRangeXZ { //radius
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float DetectRangeY { //height
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float TimeUntilTakeOff { //delay
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public BAGunShipAttack Attack { //attack
            get => (BAGunShipAttack)ReadInt(12);
            set => Write(12, (int)value);
        }

        [Description("Always 0 in original objects, purpose unknown")]
        public int int4 {  //??
            get => ReadInt(16);
            set => Write(16, value);
        }

        [Description("Enemy w/LinkID to spawn")]
        public int LinkID_EnemySpawning { //ComID
            get => ReadInt(20);
            set => Write(20, value);
        }

        [Description("Number of times to re-drop enemy from LinkID_EnemySpawning")]
        public int NumberOfDrops {
            get => ReadInt(24);
            set => Write(24, value);
        }

        [Description("Always 0 in original objects, purpose unknown")]
        public int int7 { //??
            get => ReadInt(28);
            set => Write(28, value);
        }

        public float AttackInterval { //interval
            get => ReadFloat(32);
            set => Write(32, value);
        }
    }

    public enum BAGunShipAttack {
        NONE,
        ATTACK,
        ATTACK2=5
    }
}

