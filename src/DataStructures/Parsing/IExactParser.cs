using System;

namespace QuantitativeWorld.Parsing
{
    public interface IExactParser<T>
    {
        T ParseExact(string value, IFormatProvider formatProvider);
        bool TryParseExact(string value, IFormatProvider formatProvider, out T result);
    }
}
