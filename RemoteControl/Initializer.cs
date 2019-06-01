using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl
{
    public static class Initializer
    {
        public static bool Initialized = false;
        public static void Initialize()
        {
            /* This class sets up a fallback in case a dependency/referenced
               DLL fails to load. */
            if (!Initialized)
            {
                Initialized = true;
                AppDomain.CurrentDomain.AssemblyResolve += LocalAssemblyFinder.ResolveAppDomainAssembly;
            }
        }
    }
}
