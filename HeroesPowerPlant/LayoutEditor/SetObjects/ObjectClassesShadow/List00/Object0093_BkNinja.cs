namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0093_BkNinja : SetObjectShadow
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

        public BkNinjaAppearType AppearType
        { //7
            get => (BkNinjaAppearType)ReadInt(28);
            set => Write(28, (int)value);
        }

        public int ShootCount
        { //8
            get => ReadInt(32);
            set => Write(32, value);
        }

        public float AttackInterval
        { //9
            get => ReadFloat(36);
            set => Write(36, value);
        }

        public float WaitInterval
        { //10
            get => ReadFloat(40);
            set => Write(40, value);
        }
        public float Pos0_X
        { //11
            get => ReadFloat(44);
            set => Write(44, value);
        }
        public float Pos0_Y
        { //12
            get => ReadFloat(48);
            set => Write(48, value);
        }
        public float Pos0_Z
        { //13
            get => ReadFloat(52);
            set => Write(52, value);
        }
        public int UNUSED_Pos0_IntWaitType
        { //14
            get => ReadInt(56);
            set => Write(56, value);
        }
        public float UNUSED_Pos0_DisappearTime
        { //15
            get => ReadFloat(60);
            set => Write(60, value);
        }
        public float UNUSED_Pos1_X
        { //16
            get => ReadFloat(64);
            set => Write(64, value);
        }

        public float UNUSED_Pos1_Y
        { //17
            get => ReadFloat(68);
            set => Write(68, value);
        }

        public float UNUSED_Pos1_Z
        { //18
            get => ReadFloat(72);
            set => Write(72, value);
        }

        public float UNUSED_Pos1_WaitType
        { //19
            get => ReadFloat(76);
            set => Write(76, value);
        }

        public float UNUSED_Pos1_DisappearTime
        { //20
            get => ReadFloat(80);
            set => Write(80, value);
        }

        public float UNUSED_Float21
        { //21
            get => ReadFloat(84);
            set => Write(84, value);
        }

        public float UNUSED_Float22
        { //22
            get => ReadFloat(88);
            set => Write(88, value);
        }
    }

    public enum BkNinjaAppearType
    {
        STAND,
        APPEAR_WHEN_CLOSE,
        WARP,
        ON_AIR_SAUCER_WARP
    }
}
