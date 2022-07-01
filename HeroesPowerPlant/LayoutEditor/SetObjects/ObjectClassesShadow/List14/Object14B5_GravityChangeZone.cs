using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object14B5_GravityChangeZone : SetObjectShadow
    {
        // Also used for 14B4, GravityChangeSwitch
        [Description("Sets the base gravity. By default the world is NegY")]
        public GravityDirection GravityDirection
        {
            get => (GravityDirection)ReadInt(0);
            set => Write(0, (int)value);
        }
    }
}
