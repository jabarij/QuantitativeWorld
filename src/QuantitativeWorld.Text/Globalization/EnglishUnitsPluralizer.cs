using Common.Internals.DotNetExtensions;
using System.Collections.Generic;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Globalization
{
#else
namespace QuantitativeWorld.Text.Globalization
{
#endif
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
