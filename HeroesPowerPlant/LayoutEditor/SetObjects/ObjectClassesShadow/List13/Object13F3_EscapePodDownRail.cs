namespace HeroesPowerPlant.LayoutEditor
{
    public class Object13F3_EscapePodDownRail : SetObjectShadow
    {
        //EscapePlaneRail(dist,speed)
        public float Distance
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float Speed
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
    }
}
