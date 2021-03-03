using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Parsing
{
#else
namespace QuantitativeWorld.Text.Parsing
{
#endif
    public interface IFormattedParser<T>
    {
        T ParseExact(string value, string format, IFormatProvider formatProvider);
        bool TryParseExact(string value, string format, IFormatProvider formatProvider, out T result);
    }
}
