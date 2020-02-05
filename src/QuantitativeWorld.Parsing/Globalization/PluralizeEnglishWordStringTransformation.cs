using QuantitativeWorld.DotNetExtensions;
using System.Linq;

namespace QuantitativeWorld.Text.Globalization
{
    class PluralizeEnglishWordStringTransformation : ITransformation<string>
    {
        public string Transform(string value)
        {
            if (value == null)
                return value;

            if (value.EndsWithWhiteSpace())
                value = value.TrimEnd();

            bool toUpper = char.IsUpper(value.Last());
            string pluralizedName;
            if (value.EndsWithAny(true, 's', 'x')
                || value.EndsWithAny("ch"))
            {
                string suffix = toUpper ? "ES" : "es";
                pluralizedName = string.Concat(value, suffix);
            }
            else if (value.EndsWithAny(true, 'y'))
            {
                string nameWithoutY = value.SubstringBefore(value.Length - 1);
                string suffix = toUpper ? "IES" : "ies";
                pluralizedName = string.Concat(nameWithoutY, suffix);
            }
            else
                pluralizedName = string.Concat(value, toUpper ? "S" : "s");

            return pluralizedName;
        }
    }
}
