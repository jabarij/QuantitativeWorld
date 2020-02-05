using System;

namespace QuantitativeWorld.Text.Formatting
{
    public interface ICustomFormatter<T> : ICustomFormatter
    {
        string Format(string format, T arg, IFormatProvider formatProvider);
        string Format(string format, T arg);
    }
}