using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Reloaded.Hooks;
using Reloaded.Hooks.X86;

namespace RemoteControl
{
    /// <summary>
    /// Exposes <see cref="Queue{T}"/>s of functions to call that you can append to.
    /// The functions you queue are called at specific intervals depending on the queue subscribed (e.g. When game draws HUD). 
    /// </summary>
    public static class Queue
    {
        /* Function Addresses */
        private const int DrawHudAddress = 0x0041DFD0;
        private static readonly object lockObject = new object();

        /// <summary>
        /// A <see cref="ConcurrentQueue{T}"/> that executes all methods in it when the game draws the onscreen HUD.
        /// </summary>
        public static ConcurrentQueue<Action> DrawHudQueue = new ConcurrentQueue<Action>();
        private static IHook<DrawHUD> DrawHudHook;

        /// <summary>
        /// Hooks the game Draw HUD function
        /// </summary>
        static Queue()
        {
            DrawHudHook = new Hook<DrawHUD>(DrawHudHookImpl, DrawHudAddress);
            DrawHudHook.Activate();
        }

        private static int DrawHudHookImpl()
        {
            lock (lockObject)
            {
                while (DrawHudQueue.TryDequeue(out Action item))
                {
                    item();
                }

                return DrawHudHook.OriginalFunction();
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [Function(CallingConventions.Cdecl)]
        private delegate int DrawHUD();
    }
}
