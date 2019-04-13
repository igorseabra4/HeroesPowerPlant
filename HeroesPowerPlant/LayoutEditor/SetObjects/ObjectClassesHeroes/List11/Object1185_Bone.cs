namespace HeroesPowerPlant.LayoutEditor
{
    public enum BoneType
    {
        FromRight = 0,
        FromLeft = 1,
        FromBelow = 2
    }

    public class Object1185_Bone : SetObjectManagerHeroes
    {
        public BoneType Type
        {
            get => (BoneType)ReadLong(4);
            set => Write(4, (int)value);
        }
    }
}
