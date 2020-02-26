using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.DotNetExtensions
{
    internal static class Assert
    {
        public static T IsGreaterThan<T>(T value, T min, string paramName) where T : struct, IComparable<T>
        {
            if (!Check.IsGreaterThan(value, min))
                throw Error.ArgumentNotGreaterThan(value, min, paramName);
            return value;
        }

        public static T IsGreaterThanOrEqual<T>(T value, T min, string paramName) where T : struct, IComparable<T>
        {
            if (!Check.IsGreaterThanOrEqual(value, min))
                throw Error.ArgumentNotGreaterThanOrEqual(value, min, paramName);
            return value;
        }
        public static T IsLowerThan<T>(T value, T max, string paramName) where T : struct, IComparable<T>
        {
            if (!Check.IsLowerThan(value, max))
                throw Error.ArgumentNotLowerThan(value, max, paramName);
            return value;
        }
        public static T IsLowerThanOrEqual<T>(T value, T max, string paramName) where T : struct, IComparable<T>
        {
            if (!Check.IsLowerThanOrEqual(value, max))
                throw Error.ArgumentNotLowerThanOrEqual(value, max, paramName);
            return value;
        }
        public static T IsInRange<T>(T value, T min, T max, string paramName) where T : struct, IComparable<T>
        {
            if (!Check.IsInRange(value, min, max))
                throw Error.ArgumentNotInRange(value, min, max, paramName);
            return value;
        }
        public static T IsBetween<T>(T value, T min, T max, string paramName) where T : struct, IComparable<T>
        {
            if (!Check.IsBetween(value, min, max))
                throw Error.ArgumentNotBetween(value, min, max, paramName);
            return value;
        }

        public static T? IsNullOrGreaterThan<T>(T? value, T min, string paramName) where T : struct, IComparable<T>
        {
            if (!Check.IsNullOrGreaterThan(value, min))
                throw Error.ArgumentNotNullOrGreaterThan(value, min, paramName);
            return value;
        }
        public static T? IsNullOrGreaterThanOrEqual<T>(T? value, T min, string paramName) where T : struct, IComparable<T>
        {
            if (!Check.IsNullOrGreaterThanOrEqual(value, min))
                throw Error.ArgumentNotNullOrGreaterThanOrEqual(value, min, paramName);
            return value;
        }
        public static T? IsNullOrLowerThan<T>(T? value, T max, string paramName) where T : struct, IComparable<T>
        {
            if (!Check.IsNullOrLowerThan(value, max))
                throw Error.ArgumentNotNullOrLowerThan(value, max, paramName);
            return value;
        }
        public static T? IsNullOrLowerThanOrEqual<T>(T? value, T max, string paramName) where T : struct, IComparable<T>
        {
            if (!Check.IsNullOrLowerThanOrEqual(value, max))
                throw Error.ArgumentNotNullOrLowerThanOrEqual(value, max, paramName);
            return value;
        }
        public static T? IsNullOrInRange<T>(T? value, T min, T max, string paramName) where T : struct, IComparable<T>
        {
            if (!Check.IsNullOrInRange(value, min, max))
                throw Error.ArgumentNotNullOrInRange(value, min, max, paramName);
            return value;
        }
        public static T? IsNullOrBetween<T>(T? value, T min, T max, string paramName) where T : struct, IComparable<T>
        {
            if (!Check.IsNullOrBetween(value, min, max))
                throw Error.ArgumentNotNullOrBetween(value, min, max, paramName);
            return value;
        }

        public static T? IsNotNullAndGreaterThan<T>(T? value, T min, string paramName) where T : struct, IComparable<T> =>
            IsGreaterThan(IsNotNull(value, paramName).Value, min, paramName);
        public static T? IsNotNullAndGreaterThanOrEqual<T>(T? value, T min, string paramName) where T : struct, IComparable<T> =>
            IsGreaterThanOrEqual(IsNotNull(value, paramName).Value, min, paramName);
        public static T? IsNotNullAndLowerThan<T>(T? value, T min, string paramName) where T : struct, IComparable<T> =>
            IsLowerThan(IsNotNull(value, paramName).Value, min, paramName);
        public static T? IsNotNullAndLowerThanOrEqual<T>(T? value, T min, string paramName) where T : struct, IComparable<T> =>
            IsLowerThanOrEqual(IsNotNull(value, paramName).Value, min, paramName);
        public static T? IsNotNullAndInRange<T>(T? value, T min, T max, string paramName) where T : struct, IComparable<T> =>
            IsInRange(IsNotNull(value, paramName).Value, min, max, paramName);
        public static T? IsNotNullAndBetween<T>(T? value, T min, T max, string paramName) where T : struct, IComparable<T> =>
            IsBetween(IsNotNull(value, paramName).Value, min, max, paramName);

        public static T IsNotNull<T>(T value, string paramName)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
            return value;
        }
        public static T IsNotNull<T>(T value, Func<Exception> exception)
        {
            if (value == null)
                throw exception();
            return value;
        }

        public static string IsNotNullOrEmpty(string value, string paramName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(paramName);
            return value;
        }
        public static string IsNotNullOrWhiteSpace(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(paramName);
            return value;
        }

        public static IEnumerable<T> IsNotNullOrEmpty<T>(IEnumerable<T> collection, string paramName) =>
            IsNotNullOrEmpty(collection, () => new ArgumentNullException(paramName));
        public static IEnumerable<T> IsNotNullOrEmpty<T>(IEnumerable<T> collection, Func<Exception> exception = null)
        {
            if (collection == null || !collection.Any())
            {
                var ex = exception?.Invoke() ?? new ArgumentNullException(nameof(collection));
                throw ex;
            }

            return collection;
        }
        public static IEnumerable<T> HasAtLeast<T>(IEnumerable<T> collection, int count, Func<Exception> exception) =>
            collection.HasAtLeast(count)
            ? collection
            : throw exception();
        public static IEnumerable<T> HasAtMost<T>(IEnumerable<T> collection, int count, Func<Exception> exception) =>
            collection.HasAtMost(count)
            ? collection
            : throw exception();

        public static T MeetsCondition<T>(T value, Func<bool> condition, Func<Exception> exception) =>
            condition()
            ? value
            : throw exception();

        public static T MatchesPredicate<T>(T value, Predicate<T> predicate, Func<Exception> exception) =>
            predicate(value)
            ? value
            : throw exception();

        public static T MatchesPredicate<T>(T value, Predicate<T> predicate, Func<T, Exception> exception) =>
            predicate(value)
            ? value
            : throw exception(value);

        public static double IsNotNaN(double value, string paramName) =>
            MatchesPredicate(value, e => !double.IsNaN(e), () => new ArgumentException("Double precision value is NaN.", paramName));
    }
}
