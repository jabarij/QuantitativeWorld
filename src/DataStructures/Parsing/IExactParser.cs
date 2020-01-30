using System;

namespace DataStructures.Parsing
{
    public interface IExactParser<T>
    {
        T ParseExact(string value, IFormatProvider formatProvider);
        bool TryParseExact(string value, IFormatProvider formatProvider, out T result);
    }
}
