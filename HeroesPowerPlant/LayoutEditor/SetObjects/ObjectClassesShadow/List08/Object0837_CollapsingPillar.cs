namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0837_CollapsingPillar : SetObjectShadow
    {
        //PoleHolder
        public PoleHolderType PillarFallType
        {
            get => (PoleHolderType)ReadInt(0);
            set => Write(0, (int)value);
        }
        public PoleHolderFallType DirectionToFall
        {
            get => (PoleHolderFallType)ReadInt(4);
            set => Write(4, (int)value);
        }
        public float DetectRadius
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float DetectHeight
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float RollingSpeed
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float RollingDistance
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public PoleHolderPieceStrengthType Stone_2
        {
            get => (PoleHolderPieceStrengthType)ReadInt(24);
            set => Write(24, (int)value);
        }

        public PoleHolderPieceStrengthType Stone_3
        {
            get => (PoleHolderPieceStrengthType)ReadInt(28);
            set => Write(28, (int)value);
        }
        public PoleHolderPieceStrengthType Stone_4
        {
            get => (PoleHolderPieceStrengthType)ReadInt(32);
            set => Write(32, (int)value);
        }

        public PoleHolderPieceStrengthType Stone_5
        {
            get => (PoleHolderPieceStrengthType)ReadInt(36);
            set => Write(36, (int)value);
        }
        public PoleHolderPieceStrengthType Stone_6
        {
            get => (PoleHolderPieceStrengthType)ReadInt(40);
            set => Write(40, (int)value);
        }
    }

    public enum PoleHolderType
    {
        Flat,
        Slope,
        Air
    }

    public enum PoleHolderFallType
    {
        Left,
        Right
    }

    public enum PoleHolderPieceStrengthType
    {
        Soft,
        Hard
    }
}
