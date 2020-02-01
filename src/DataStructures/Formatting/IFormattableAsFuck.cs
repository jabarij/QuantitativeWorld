using System;

namespace QuantitativeWorld.Formatting.Interfaces
{
    public interface IFormattableAsFuck : IFormattable
    {
        string ToString();
        string ToString(string format);
        string ToString(IFormatProvider formatProvider);
    }
}