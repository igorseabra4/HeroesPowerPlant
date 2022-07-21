using Heroes.SDK.Utilities.Math.Structs;

namespace HeroesPowerPlant
{
    public static class Extensions
    {
        public static SharpDX.Vector3 ToSharpDXVector3(this Vector3 v)
        {
            return new SharpDX.Vector3(v.X, v.Y, v.Z);
        }

        public static Vector3 ToHeroesSDKVector(this SharpDX.Vector3 v)
        {
            return new Vector3 { X = v.X, Y = v.Y, Z = v.Z };
        }
    }
}
