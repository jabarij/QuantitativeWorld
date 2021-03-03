#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Globalization
{
#else
namespace QuantitativeWorld.Text.Globalization
{
#endif
    internal interface IPluralizer
    {
        string Pluralize(string word);
    }
}