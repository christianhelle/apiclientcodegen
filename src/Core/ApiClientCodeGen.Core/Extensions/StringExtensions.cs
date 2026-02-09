using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Rapicgen.Core.Extensions
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

        public static string ToSha256(this string value)
        {
            var stringBuilder = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                var enc = Encoding.UTF8;
                var result = hash.ComputeHash(enc.GetBytes(value));

                foreach (var b in result)
                    stringBuilder.Append(b.ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public static string GetDescription<T>(this T e) where T : Enum, IConvertible
        {
            var type = e.GetType();
            var values = Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val == e.ToInt32(CultureInfo.InvariantCulture))
                {
                    var enumName = type.GetEnumName(val);
                    if (enumName == null)
                        continue;
                        
                    var memInfo = type.GetMember(enumName);
                    if (memInfo.Length == 0)
                        continue;
                        
                    var descriptionAttribute = memInfo[0]
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() as DescriptionAttribute;

                    if (descriptionAttribute != null)
                    {
                        return descriptionAttribute.Description;
                    }
                }
            }

            throw new InvalidEnumArgumentException(
                $"{e} is not a valid value for enum {type.Name}");
        }
    }
}
