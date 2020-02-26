using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.DotNetExtensions
{
    internal static class StringExtensions
    {
        public static string LowercaseFirstLetter(this string value)
        {
            if (!StartsWith(value, char.IsUpper))
                return value;

            char[] valueCharacters = value.ToCharArray();
            valueCharacters[0] = char.ToLower(valueCharacters[0]);
            return new string(valueCharacters);
        }

        public static string UppercaseFirstLetter(this string value)
        {
            if (!StartsWith(value, char.IsLower))
                return value;

            char[] valueCharacters = value.ToCharArray();
            valueCharacters[0] = char.ToUpper(valueCharacters[0]);
            return new string(valueCharacters);
        }

        public static bool ContainsAt(this string value, string pattern, int index)
        {
            char[] valueCharsAt = ToCharArray(value, index, pattern.Length);
            char[] patternChars = pattern.ToCharArray();
            return valueCharsAt.ArrayEqual(patternChars);
        }

        public static bool StartsWith(this string value, Predicate<char> characterCheck) =>
            !string.IsNullOrEmpty(value) && characterCheck(value.First());

        public static bool EndsWith(this string value, Predicate<char> characterCheck) =>
            !string.IsNullOrEmpty(value) && characterCheck(value.Last());

        public static bool EndsWithAny(this string value, params char[] characters) =>
            EndsWithAny(value, false, characters);

        public static bool EndsWithAny(this string value, bool ignoreCase, params char[] characters)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            char lastChar = value.Last();
            return
                ignoreCase
                ? characters.Any(c => c.Equals(char.ToLower(lastChar)) || c.Equals(char.ToUpper(lastChar)))
                : characters.Any(c => c.Equals(lastChar));
        }

        public static bool EndsWithAny(this string value, bool ignoreCase, params string[] suffixes)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            var comparisonType =
                ignoreCase
                ? StringComparison.OrdinalIgnoreCase
                : StringComparison.Ordinal;
            return suffixes.Any(s => value.EndsWith(s, comparisonType));
        }

        public static bool EndsWithAny(this string value, params string[] suffixes) =>
            EndsWithAny(value, false, suffixes);

        public static bool EndsWithWhiteSpace(this string value) =>
            EndsWith(value, char.IsWhiteSpace);
        public static char[] ToCharArray(this string value, int startIndex, int length)
        {
            char[] result = new char[length];
            for (int index = 0; index < length; index++)
                result[index] = value[index + startIndex];
            return result;
        }

        public static string SubstringBefore(this string value, int index)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (index < 1 || value.Length <= index)
                return string.Empty;

            return value.Substring(0, index);
        }

        public static int IndexOf(this string value, Predicate<char> charMatch)
        {
            if (string.IsNullOrEmpty(value))
                return -1;

            int index = -1;
            while (index++ < value.Length)
                if (charMatch(value[index]))
                    return index;
            return -1;
        }

        public static int LastIndexOf(this string value, Predicate<char> charMatch)
        {
            if (string.IsNullOrEmpty(value))
                return -1;

            int index = value.Length;
            while (index-- > 0)
                if (charMatch(value[index]))
                    return index;
            return -1;
        }

        public static IEnumerable<int> IndexesOf(this string value, Predicate<char> charMatch)
        {
            if (!string.IsNullOrEmpty(value))
            {
                int index = -1;
                while (index++ < value.Length)
                    if (charMatch(value[index]))
                        yield return index;
            }
        }

        public static IEnumerable<int> IndexesOfBack(this string value, Predicate<char> charMatch)
        {
            if (!string.IsNullOrEmpty(value))
            {
                int index = value.Length;
                while (index-- > -1)
                    if (charMatch(value[index]))
                        yield return index;
            }
        }

        public static IEnumerable<string> SplitBy(this string value, Predicate<char> charMatch)
        {
            if (!string.IsNullOrEmpty(value))
            {
                int endIndex = value.Length - 1;
                foreach (int startIndex in IndexesOfBack(value, charMatch))
                    yield return value.Substring(startIndex, endIndex - startIndex + 1);
            }
        }

        public static (string left, string right) SplitAt(this string value, int rightStartIndex)
        {
            Assert.IsNotNullOrEmpty(value, nameof(value));
            if (rightStartIndex >= value.Length)
                throw Error.ArgumentOutOfRange(nameof(rightStartIndex), $"Index is out of range.");

            char[] leftPart = new char[rightStartIndex];
            for (int index = 0; index < leftPart.Length; index++)
                leftPart[index] = value[index];

            char[] rightPart = new char[value.Length - rightStartIndex - 1];
            for (int index = 0; index < rightPart.Length; index++)
                rightPart[index] = value[index + rightStartIndex];

            return (new string(leftPart), new string(rightPart));
        }
    }
}
