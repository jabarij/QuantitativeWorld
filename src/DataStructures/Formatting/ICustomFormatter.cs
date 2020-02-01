using System;

namespace QuantitativeWorld.Formatting
{
    public interface ICustomFormatter<T> : ICustomFormatter
    {
        string Format(string format, T arg, IFormatProvider formatProvider);
        string Format(string format, T arg);
    }
}