using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07D8_LaserWallBarrier : SetObjectShadow
    {
        //ElecBarrier

        [Description("Note: One sided view only, currently opposite of placement\n Move around to see model in editor")]
        public BarrierType Model
        {
            get => (BarrierType)ReadInt(0);
            set => Write(0, (int)value);
        }
    }

    public enum BarrierType
    {
        Model0,
        Model1,
        Model2
    }
}
