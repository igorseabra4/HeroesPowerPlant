using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000B_DashPanel : SetObjectHeroes
    {
        [Description("Defaults to 5.0")]
        public float Speed
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        [Description("In frames")]
        public short NoControlTime
        {
            get => ReadShort(8);
            set => Write(8, value);
        }
    }
}