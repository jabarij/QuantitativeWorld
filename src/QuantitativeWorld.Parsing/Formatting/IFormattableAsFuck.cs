using System;

namespace QuantitativeWorld.Text.Formatting.Interfaces
{
    public interface IFormattableAsFuck : IFormattable
    {
        string ToString();
        string ToString(string format);
        string ToString(IFormatProvider formatProvider);
    }
}