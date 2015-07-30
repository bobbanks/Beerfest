using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Beerfest.Core.Infrastructure {

    public static class StringExtensions {

        public static string ConvertCrlfToBreaks(this string s) {
            return s.IsNullOrWhiteSpace() ? s : s.Replace("\n", "<br/>");
        }

        public static string SantizeFilename(this string filename) {
            return Path.GetInvalidFileNameChars().Aggregate(filename, (current, c) => current.Replace(c.ToString(), string.Empty));
        }

        public static string Slugify(this string phrase)
        {
            string str = phrase.ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[']", "");
            str = Regex.Replace(str, @"[^a-z0-9\s-]", " ");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public static bool IsNullOrWhiteSpace(this string value) {
            return string.IsNullOrWhiteSpace(value);
        }

        public static T? ToNullable<T>(this string s) where T : struct {

            if (s.IsNullOrWhiteSpace()) return null;

            var result = new T?();

            if (!string.IsNullOrWhiteSpace(s)) {
                var conv = TypeDescriptor.GetConverter(typeof (T));
                result = (T) conv.ConvertFrom(s);
            }

            return result;
        }

        public static string NullIfWhitespace(this string text) {
            return (string.IsNullOrWhiteSpace(text) ? null : text);
        }


    }

}