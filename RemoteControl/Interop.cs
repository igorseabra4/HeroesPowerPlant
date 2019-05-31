using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl
{
    /// <summary>
    /// Contains the various structures used to call individual functions.
    /// </summary>
    public class Interop
    {
        public struct NativeString
        {
            [MarshalAs(UnmanagedType.AnsiBStr)]
            public string String;
        }
    }
}
