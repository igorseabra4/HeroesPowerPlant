using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object07EB_CubePlatformCircle : SetObjectShadow
    {
        //ElecFootingRotate(OBJ_NUM, RADIUS m, CIRCLE_SPD deg/sec)

        [Description("This objects spawns this many cubes, that orbit around Radius at CircleSpeed")]
        public int NumberOfCubes
        {
            get => ReadInt(0);
            set => Write(0, value);
        }

        [Description("m")]
        public float Radius
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        [Description("deg/sec")]
        public float CircleSpeed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}
