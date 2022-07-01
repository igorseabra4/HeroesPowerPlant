namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0092_BkChaos : SetObjectShadow
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
        // end EnemyBase

        public float Health
        { //6
            get => ReadFloat(24);
            set => Write(24, value);
        }

        public BkChaosShapeType StartingState
        { //7 ShapeType
            get => (BkChaosShapeType)ReadInt(28);
            set => Write(28, (int)value);
        }

        public int NumberOfChibi
        { //8
            get => ReadInt(32);
            set => Write(32, value);
        }

        public float BrokenPieceFlyDistance
        { //9
            get => ReadFloat(36);
            set => Write(36, value);
        }

        public float BrokenPieceFlyOffset
        { //10
            get => ReadFloat(40);
            set => Write(40, value);
        }

        public float CombineStartTime
        { //11
            get => ReadFloat(44);
            set => Write(44, value);
        }

        public float CombineTime
        { //12
            get => ReadFloat(48);
            set => Write(48, value);
        }
    }

    public enum BkChaosShapeType
    {
        BrokenUp,
        Complete
    }
}
