using System.Runtime.InteropServices;

namespace RemoteControl.Structs
{
    /// <summary>
    /// Contains the various structures used to call individual functions.
    /// </summary>
    public struct NativeString64Char
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string String;

        public NativeString64Char(string s)
        {
            String = s;
        }
    }
}
