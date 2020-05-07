using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object002C_RollDoor : SetObjectHeroes
    {
        [Description("Defaults to 5.0")]
        public float Power
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        [Description("In degrees")]
        public float Elevation
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("In frames")]
        public short NoControlTime
        {
            get => ReadShort(12);
            set => Write(12, value);
        }
    }
}