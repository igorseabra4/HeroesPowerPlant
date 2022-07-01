using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1772_ConcreteDoor : SetObjectShadow
    {
        public float Detect_X
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float Detect_Y
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float Detect_Z
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("m/s to close\n smaller = slower")]
        public float CloseSpeed
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}
