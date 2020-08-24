using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions
{
    public static class StringExtension
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new List<JsonConverter> { new StringEnumConverter() }
        };

        public static IntPtr ConvertToIntPtr(this string code, out uint pcbOutput)
        {
            var data = Encoding.Default.GetBytes(code);

            var ptr = Marshal.AllocCoTaskMem(data.Length);
            Marshal.Copy(data, 0, ptr, data.Length);

            pcbOutput = (uint)data.Length;
            return ptr;
        }

        public static string ToJson(this object value)
            => JsonConvert.SerializeObject(
                value,
                JsonSettings);

        public static bool EndsWithAny(this string text, params string[] words) 
            => EndsWithAny(text, words, StringComparison.CurrentCultureIgnoreCase);

        public static bool EndsWithAny(
            this string text,
            IEnumerable<string> words,
            StringComparison comparisonType)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            if (words == null) throw new ArgumentNullException(nameof(words));
            return words.Any(word => text.EndsWith(word, comparisonType));
        }
    }
}
