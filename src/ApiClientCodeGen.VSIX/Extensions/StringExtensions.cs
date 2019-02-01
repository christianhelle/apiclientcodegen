using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions
{
    public static class StringExtension
    {
        public static IntPtr ConvertToIntPtr(this string code, out uint pcbOutput)
        {
            var data = Encoding.Default.GetBytes(code);

            var ptr = Marshal.AllocCoTaskMem(data.Length);
            Marshal.Copy(data, 0, ptr, data.Length);

            pcbOutput = (uint)data.Length;
            return ptr;
        }
    }
}
