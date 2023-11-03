using Reloaded.Memory.Utilities;

namespace HeroesPowerPlant.Shared.Utilities
{
    /// <summary>
    /// Provides a set of methods to manually convert the endian of each primitive type.
    /// </summary>
    public static class ManualEndianConverter
    {
        public static T ReverseEndian<T>(this T type) where T : unmanaged
        {
            Endian.Reverse(type);
            return type;
        }
    }
}
