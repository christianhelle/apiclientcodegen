using System;
using System.Runtime.InteropServices;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows
{
    internal static class NativeMethods
    {
        internal const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int SendMessage(
            IntPtr hWnd,
            int msg,
            int wParam,
            [MarshalAs(UnmanagedType.LPWStr)] string lParam);
    }
}
