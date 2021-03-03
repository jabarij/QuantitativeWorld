using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Formatting
{
#else
namespace QuantitativeWorld.Text.Formatting
{
#endif
    public interface ICustomFormatter<T> : ICustomFormatter
    {
        string Format(string format, T arg, IFormatProvider formatProvider);
        string Format(string format, T arg);
    }
}