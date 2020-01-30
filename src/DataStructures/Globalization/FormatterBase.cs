using System;

namespace DataStructures.Globalization
{
    public abstract class FormatterBase<T> : ICustomFormatter<T>, IFormatProvider, ICustomFormatter
    {
        public virtual string Format(string format, T arg, IFormatProvider formatProvider) =>
            TryFormat(format, arg, formatProvider, out string result)
            ? result
            : throw GetUnknownFormatException(format);
        public virtual string Format(string format, T arg) =>
            Format(format, arg, this);

        public abstract bool TryFormat(string format, T weight, IFormatProvider formatProvider, out string result);

        public string Format(string format, object arg, IFormatProvider formatProvider) =>
            Format(format, (T)arg, formatProvider);

        public object GetFormat(Type formatType) =>
            GetFormatOrNull(formatType);

        protected virtual object GetFormatOrNull(Type formatType) =>
            formatType == typeof(ICustomFormatter)
            || formatType == typeof(ICustomFormatter<T>)
            ? this
            : null;

        protected internal virtual FormatException GetUnknownFormatException(string unknownFormat) =>
            new FormatException($"Unknown format '{unknownFormat}'.");
    }
}