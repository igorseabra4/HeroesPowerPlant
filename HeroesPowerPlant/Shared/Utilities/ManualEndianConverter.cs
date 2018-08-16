using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using HeroesONE_R.Utilities;

namespace HeroesPowerPlant.Shared.Utilities
{
    /// <summary>
    /// Provides a set of methods to manually convert the endian of each primitive type.
    /// </summary>
    public static class ManualEndianConverter
    {
        /// <summary>
        /// Reads a value from the CLFile array and retrieves it in the desired format.
        /// </summary>
        /// <returns></returns>
        public static T ReverseEndian<T>(this T type) where T : unmanaged
        {
            // Declare an array for storing the data.
            byte[] data = StructUtilities.ConvertStructureToByteArrayUnsafe(ref type).Reverse().ToArray();

            // Use this base object for the storage of the value we are retrieving.
            return StructUtilities.ArrayToStructureUnsafe<T>(ref data);
        }

        /// <summary>
        /// GetManagedSize returns the size of a structure whose type
        /// is 'type', as stored in managed memory. For any reference type
        /// this will simply return the size of a pointer (4 or 8).
        /// </summary>
        public static int GetManagedSize(Type type)
        {
            var method = new DynamicMethod("GetManagedSizeImpl", typeof(uint), new Type[0], typeof(ManualEndianConverter), false);

            ILGenerator gen = method.GetILGenerator();
            gen.Emit(OpCodes.Sizeof, type);
            gen.Emit(OpCodes.Ret);

            var func = (Func<uint>)method.CreateDelegate(typeof(Func<uint>));
            return checked((int)func());
        }
    }
}
