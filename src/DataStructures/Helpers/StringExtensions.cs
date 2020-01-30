using System;
using System.Linq;

namespace Plant.QAM.BusinessLogic.PublishedLanguage.Strings
{
    public static class StringExtensions
    {
        public static string UppercaseFirstLetter(this string value)
        {
            if (!StartsWith(value, char.IsLower))
                return value;

            char[] valueCharacters = value.ToCharArray();
            valueCharacters[0] = char.ToUpper(valueCharacters[0]);
            return new string(valueCharacters);
        }

        public static string LowercaseFirstLetter(this string value)
        {
            if (!StartsWith(value, char.IsUpper))
                return value;

            char[] valueCharacters = value.ToCharArray();
            valueCharacters[0] = char.ToLower(valueCharacters[0]);
            return new string(valueCharacters);
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

        public static bool EndsWithWhiteSpace(this string value) =>
            EndsWith(value, char.IsWhiteSpace);

        public static string SubstringBefore(this string value, int index)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (index < 1 || value.Length <= index)
                return string.Empty;

            return value.Substring(0, index);
        }
    }
}
