namespace HeroesPowerPlant.LayoutEditor
{
    public class Object13ED_EscapePodPathSwitch : SetObjectShadow
    {
        //SetRailSwitch(route)
        public int SplineID
        {
            get => ReadInt(0);
            set => Write(0, value);
        }
    }
}
