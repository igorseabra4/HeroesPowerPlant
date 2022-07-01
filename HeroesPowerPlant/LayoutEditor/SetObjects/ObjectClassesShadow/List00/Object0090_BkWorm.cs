namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0090_BkWorm : SetObjectShadow
    {

        // Modified EnemyBase
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

        public int SearchAngle
        { //2 (worms use int instead of float here)
            get => ReadInt(8);
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
        // end modified EnemyBase

        public BkWormType WormType
        { //7
            get => (BkWormType)ReadInt(28);
            set => Write(28, (int)value);
        }

        public int AttackCount
        { //8
            get => ReadInt(32);
            set => Write(32, value);
        }

        public float AppearDelay
        { //9
            get => ReadFloat(36);
            set => Write(36, value);
        }
    }

    public enum BkWormType
    {
        BLACK,
        BLUE,
        GOLD,
    }
}
