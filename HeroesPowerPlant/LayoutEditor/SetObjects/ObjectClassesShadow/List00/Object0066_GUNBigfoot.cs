namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0066_GUNBigfoot : SetObjectShadow
    {

        // EnemyBase
        public float MoveRange
        { //0
            get => ReadFloat(0);
            set => Write(0, value);
        }

        public float SearchRange
        { //1
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float SearchAngle
        { //2
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float SearchWidth
        { //3
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float SearchHeight
        { //4
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float SearchHeightOffset
        { //5
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public float MoveSpeedRatio
        { //6
            get => ReadFloat(24);
            set => Write(24, value);
        }
        // end EnemyBase

        public GUNBigfootAppearType AppearType
        { //7
            get => (GUNBigfootAppearType)ReadInt(28);
            set => Write(28, (int)value);
        }

        public GUNBigfootWeaponType WeaponType
        { //8
            get => (GUNBigfootWeaponType)ReadInt(32);
            set => Write(32, (int)value);
        }

        public float OffsetPos_Y
        { //9
            get => ReadFloat(36);
            set => Write(36, value);
        }
    }

    public enum GUNBigfootAppearType
    {
        WAIT_CROUCH,
        HOVERING,
        DROP,
        ACT_ATTACKING,
        ZUTTO_HOVERING
    }

    public enum GUNBigfootWeaponType
    {
        VULCAN,
        MISSILE
    }
}
