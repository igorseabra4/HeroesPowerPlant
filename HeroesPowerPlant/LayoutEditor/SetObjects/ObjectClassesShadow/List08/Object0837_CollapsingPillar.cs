namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0837_CollapsingPillar : SetObjectShadow
    {
        public enum EObjectType : int
        {
            Flat,
            Slope,
            Air
        }

        public enum EFallDirection : int
        {
            Left,
            Right
        }

        public enum PoleHolderPieceStrengthType : int
        {
            Soft,
            Hard
        }

        //PoleHolder

        [MiscSetting]
        public EObjectType ObjectType { get; set; }
        [MiscSetting]
        public EFallDirection FallDirection { get; set; }
        [MiscSetting]
        public float DetectRadius { get; set; }
        [MiscSetting]
        public float DetectHeight { get; set; }
        [MiscSetting]
        public float RollingSpeed { get; set; }
        [MiscSetting]
        public float RollingDistance { get; set; }
        [MiscSetting]
        public PoleHolderPieceStrengthType Stone_2 { get; set; }
        [MiscSetting]
        public PoleHolderPieceStrengthType Stone_3 { get; set; }
        [MiscSetting]
        public PoleHolderPieceStrengthType Stone_4 { get; set; }
        [MiscSetting]
        public PoleHolderPieceStrengthType Stone_5 { get; set; }
        [MiscSetting]
        public PoleHolderPieceStrengthType Stone_6 { get; set; }
    }
}
