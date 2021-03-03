using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Parsing
{
#else
namespace QuantitativeWorld.Text.Parsing
{
#endif
    public interface IExactParser<T>
    {
        T ParseExact(string value, IFormatProvider formatProvider);
        bool TryParseExact(string value, IFormatProvider formatProvider, out T result);
    }
}
