using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Formatting.Interfaces
{
#else
namespace QuantitativeWorld.Text.Formatting.Interfaces
{
#endif
    public interface IFormattableAsFuck : IFormattable
    {
        string ToString();
        string ToString(string format);
        string ToString(IFormatProvider formatProvider);
    }
}