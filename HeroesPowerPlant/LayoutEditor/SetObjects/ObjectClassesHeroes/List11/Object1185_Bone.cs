namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1185_Bone : SetObjectHeroes
    {
        public enum EBoneType : int
        {
            FromRight = 0,
            FromLeft = 1,
            FromBelow = 2
        }

        [MiscSetting]
        public EBoneType BoneType { get; set; }
    }
}
