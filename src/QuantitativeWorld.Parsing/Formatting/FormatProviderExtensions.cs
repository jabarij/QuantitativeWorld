using System;

namespace QuantitativeWorld.Text.Formatting
{
    static class FormatProviderExtensions
    {
        public static T GetFormat<T>(this IFormatProvider formatProvider) =>
            (T)formatProvider.GetFormat(typeof(T));

        public static T GetFormatProvider<T>(this IFormatProvider formatProvider)
            where T : IFormatProvider =>
            formatProvider.GetFormat<T>();
    }
}
