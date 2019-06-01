using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Reloaded.Hooks.X86;
using Reloaded.Memory.Sources;
using static Reloaded.Hooks.X86.FunctionAttribute;

namespace RemoteControl
{
    public unsafe class CollisionReloader
    {
        /// <summary>
        /// Name of the method to load the collision mid-game.
        /// </summary>
        public static string LoadCollisionFunctionName = nameof(LoadCollision);

        /// <summary>
        /// Pointer to the <see cref="InitCollision"/> function.
        /// </summary>
        private const int InitCollisionPtr = 0x00425500;

        /// <summary>
        /// Pointer to the "LoadManager" responsible for handling the currently loaded in stage.
        /// </summary>
        private const int LoadManagerPtr = 0x00A792D0;

        /// <summary>
        /// Delegate to call the internal <see cref="InitCollision"/> function.
        /// </summary>
        private static InitCollision _initCollision;

        static CollisionReloader()
        {
            Initializer.Initialize();
            Init(); // Actual constructor in there, in case uses libraries not yet loaded.
        }

        /* The real static constructor */
        private static void Init()
        {
            _initCollision = Wrapper.Create<InitCollision>(InitCollisionPtr);
        }

        /// <param name="nativeStringPtr">Pointer to a <see cref="Interop.NativeString64Char"/> with the name of the file in the collision folder minus extension e.g. "s01"</param>
        [DllExport]
        public static void LoadCollision(int nativeStringPtr)
        {
            Memory.CurrentProcess.Read((IntPtr)nativeStringPtr, out Interop.NativeString64Char nativeString, true);
            _initCollision((IntPtr)LoadManagerPtr, nativeString.String);
        }

        /// <summary>
        /// Loads a collision file.
        /// </summary>
        /// <param name="landManagerPtr">Pointer to the land manager.</param>
        /// <param name="stringPtr">Pointer to name of the file.</param>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [Function(new[] { Register.edi, Register.esi }, Register.eax, StackCleanup.None)]
        public delegate int InitCollision(IntPtr landManagerPtr, [MarshalAs(UnmanagedType.LPStr)] string stringPtr);
    }
}
