using System;

namespace DataStructures.Globalization
{
    public interface ICustomFormatter<T> : ICustomFormatter
    {
        string Format(string format, T arg, IFormatProvider formatProvider);
        string Format(string format, T arg);
    }
}