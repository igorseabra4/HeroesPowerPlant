using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000B_DashPanel : SetObjectHeroes
    {
        public float Speed
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public short NoControlTime
        {
            get => ReadShort(8);
            set => Write(8, value);
        }
    }
}