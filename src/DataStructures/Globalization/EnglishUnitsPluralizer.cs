using Plant.QAM.BusinessLogic.PublishedLanguage.Transformations;
using System.Collections.Generic;

namespace QuantitativeWorld.Globalization
{
    class EnglishUnitsPluralizer : IPluralizer
    {
        private readonly ITransformation<string> _pluralizer = new PluralizeEnglishWordStringTransformation();

        private readonly Dictionary<string, string> _irregularPlurals = new Dictionary<string, string>
        {
            { "foot", "feet" }
        };

        public string Pluralize(string word) =>
            _irregularPlurals.TryGetValue(word, out string result)
            ? result
            : _pluralizer.Transform(word);
    }
}
