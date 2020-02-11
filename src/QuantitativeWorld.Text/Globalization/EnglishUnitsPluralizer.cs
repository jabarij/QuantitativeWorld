using QuantitativeWorld.DotNetExtensions;
using System.Collections.Generic;

namespace QuantitativeWorld.Text.Globalization
{
    class DictionaryPluralizer : IPluralizer
    {
        private readonly ITransformation<string> _pluralizer;

        public DictionaryPluralizer()
            : this(new Dictionary<string, string>()) { }
        public DictionaryPluralizer(IDictionary<string, string> irregularPlurals)
        {
            Assert.IsNotNull(irregularPlurals, nameof(irregularPlurals));

            _pluralizer = new DictionaryTransformationWrapper<string>(
                dictionary: irregularPlurals,
                transformation: new PluralizeEnglishWordStringTransformation());
        }

        public string Pluralize(string word) => _pluralizer.Transform(word);
    }
}
