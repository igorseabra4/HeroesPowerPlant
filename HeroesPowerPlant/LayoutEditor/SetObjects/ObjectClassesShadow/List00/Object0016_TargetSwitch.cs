namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0016_TargetSwitch : SetObjectShadow
    {

        public float Move_X
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }

        public float Move_Y
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float Move_Z
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float MoveSpeed
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float MoveWait
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public int NumberOfHits
        {
            get => ReadInt(20);
            set => Write(20, value);
        }
    }
}
