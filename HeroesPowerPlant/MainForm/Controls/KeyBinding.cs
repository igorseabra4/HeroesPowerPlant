using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace HeroesPowerPlant.MainForm;

public record KeyBinding(Keys Key, Keys Modifiers = Keys.None)
{
    private const uint MapVirtualKeyToChar = 2;

    [DllImport("user32.dll")]
    private static extern nint GetKeyboardLayout(uint idThread);

    [DllImport("user32.dll")]
    private static extern uint MapVirtualKeyEx(uint uCode, uint uMapType, nint dwhkl);

    private static string GetKeyName(Keys key)
    {
        uint virtualKey = (uint)key & (uint)Keys.KeyCode;
        uint character = MapVirtualKeyEx(virtualKey, MapVirtualKeyToChar, GetKeyboardLayout(0));

        // MapVirtualKeyEx returns the unshifted character for the active layout.
        // A dead key is indicated by the high bit and cannot be displayed by itself.
        if ((character & 0x80000000) == 0 && character is >= 0x20 and <= char.MaxValue)
            return ((char)character).ToString();

        return key.ToString();
    }

    public override string ToString()
    {
        var result = new StringBuilder();
        if (Modifiers.HasFlag(Keys.Control))
            result.Append("Ctrl + ");
        if (Modifiers.HasFlag(Keys.Shift))
            result.Append("Shift + ");
        if (Modifiers.HasFlag(Keys.Alt))
            result.Append("Alt + ");
        result.Append(GetKeyName(Key));
        return result.ToString();
    }
}