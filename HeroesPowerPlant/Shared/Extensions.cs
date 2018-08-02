using SharpDX;
using GenericStageInjectionCommon.Structs;

namespace HeroesPowerPlant
{
    public static class Extensions
    {
        public static Vector3 ToVector3(this Vector v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        public static Vector ToVector(this Vector3 v)
        {
            return new Vector(v.X, v.Y, v.Z);
        }
    }
}
