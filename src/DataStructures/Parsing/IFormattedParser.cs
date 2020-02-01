using System;

namespace QuantitativeWorld.Parsing
{
    public interface IFormattedParser<T>
    {
        T ParseExact(string value, string format, IFormatProvider formatProvider);
        bool TryParseExact(string value, string format, IFormatProvider formatProvider, out T result);
    }
}
