using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Formatting
{
#else
namespace QuantitativeWorld.Text.Formatting
{
#endif
    static class FormatProviderExtensions
    {
        public static T GetFormat<T>(this IFormatProvider formatProvider) =>
            (T)formatProvider.GetFormat(typeof(T));

        public static T GetFormatProvider<T>(this IFormatProvider formatProvider)
            where T : IFormatProvider =>
            formatProvider.GetFormat<T>();
    }
}
