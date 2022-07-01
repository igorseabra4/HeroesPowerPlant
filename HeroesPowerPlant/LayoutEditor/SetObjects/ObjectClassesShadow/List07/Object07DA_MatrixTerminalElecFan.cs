namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07DA_MatrixTerminalElecFan : SetObjectShadow
    {
        //ElecFan(height, radius, power)
        public float Height
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float Radius
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float Power
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}
