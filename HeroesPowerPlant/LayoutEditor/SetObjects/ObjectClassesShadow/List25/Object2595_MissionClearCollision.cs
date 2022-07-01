namespace HeroesPowerPlant.LayoutEditor
{
    public class Object2595_MissionClearCollision : SetObjectShadow
    {

        public MissionType MissionType
        {
            get => (MissionType)ReadInt(0);
            set => Write(0, (int)value);
        }

        public float Radius
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
    }

    public enum MissionType
    {
        Dark,
        Normal,
        Hero
    }
}

