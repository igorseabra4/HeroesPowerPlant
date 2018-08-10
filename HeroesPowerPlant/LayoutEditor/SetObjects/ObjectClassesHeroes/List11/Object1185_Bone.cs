namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1185_Bone : SetObjectManagerHeroes
    {
        public enum BoneType
        {
            FromRight = 0,
            FromLeft = 1,
            FromBelow = 2
        }

        public BoneType Type
        {
            get { return (BoneType)ReadLong(4); }
            set { Write(4, (int)value); }
        }
    }
}
