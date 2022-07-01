using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0061_DarkSpinEntrance : SetObjectShadow
    {

        [Description("Only valid where a darkspin spline exists")]
        public float Radius
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }
    }
}
