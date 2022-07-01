using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07DF_LightspeedFirewall : SetObjectShadow
    {
        //ElecFireWall(SearchRange, InitialY)

        public float Scale_X
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float Scale_Y
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float Scale_Z
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float SearchRange
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        [Description("Move object starting point/appearance on the Y axis this amount.\n Animates to original position on detect")]
        public float Initial_Y
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }
    }
}
