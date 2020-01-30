using Plant.QAM.BusinessLogic.PublishedLanguage.Transformations;

namespace DataStructures.Globalization
{
    class EnglishPluralizer : IPluralizer
    {
        private readonly ITransformation<string> _pluralizer = new PluralizeEnglishWordStringTransformation();

        public string Pluralize(string word) =>
            _pluralizer.Transform(word);
    }
}
