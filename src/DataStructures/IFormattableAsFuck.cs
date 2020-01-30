using System;

namespace DataStructures.Globalization
{
    public interface IFormattableAsFuck : IFormattable
    {
        string ToString();
        string ToString(string format);
        string ToString(IFormatProvider formatProvider);
    }
}