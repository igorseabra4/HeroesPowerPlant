namespace HeroesPowerPlant.LayoutEditor
{
    public class Object00_SpringShadow : SetObjectManagerShadow
    {
        public float Strenght
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }

        public float ControlTime
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
    }
}
