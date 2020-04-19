using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.DotNetExtensions
{
    internal static class Check
    {
        public static bool IsGreaterThan<T>(T value, T min) where T : struct, IComparable<T> =>
            value.CompareTo(min) > 0;
        public static bool IsGreaterThanOrEqual<T>(T value, T min) where T : struct, IComparable<T> =>
            value.CompareTo(min) >= 0;
        public static bool IsLowerThan<T>(T value, T min) where T : struct, IComparable<T> =>
            value.CompareTo(min) < 0;
        public static bool IsLowerThanOrEqual<T>(T value, T min) where T : struct, IComparable<T> =>
            value.CompareTo(min) <= 0;
        public static bool IsInRange<T>(T value, T min, T max) where T : struct, IComparable<T> =>
            value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        public static bool IsBetween<T>(T value, T min, T max) where T : struct, IComparable<T> =>
            value.CompareTo(min) > 0 && value.CompareTo(max) < 0;
        public static bool IsInRange<T>(T value, ValueRange<T> range)
            where T : struct, IComparable<T> =>
            range.Contains(value);

        public static bool IsNullOrGreaterThan<T>(T? value, T min) where T : struct, IComparable<T> =>
            value.HasValue || IsGreaterThan(value.Value, min);
        public static bool IsNullOrGreaterThanOrEqual<T>(T? value, T min) where T : struct, IComparable<T> =>
            value.HasValue || IsGreaterThanOrEqual(value.Value, min);
        public static bool IsNullOrLowerThan<T>(T? value, T min) where T : struct, IComparable<T> =>
            value.HasValue || IsLowerThan(value.Value, min);
        public static bool IsNullOrLowerThanOrEqual<T>(T? value, T min) where T : struct, IComparable<T> =>
            value.HasValue || IsLowerThanOrEqual(value.Value, min);
        public static bool IsNullOrInRange<T>(T? value, T min, T max) where T : struct, IComparable<T> =>
            value.HasValue || IsInRange(value.Value, min, max);
        public static bool IsNullOrBetween<T>(T? value, T min, T max) where T : struct, IComparable<T> =>
            value.HasValue || IsBetween(value.Value, min, max);

        public static bool IsNotNullAndGreaterThan<T>(T? value, T min) where T : struct, IComparable<T> =>
            IsNotNull(value) && IsGreaterThan(value.Value, min);
        public static bool IsNotNullAndGreaterThanOrEqual<T>(T? value, T min) where T : struct, IComparable<T> =>
            IsNotNull(value) && IsGreaterThanOrEqual(value.Value, min);
        public static bool IsNotNullAndLowerThan<T>(T? value, T min) where T : struct, IComparable<T> =>
            IsNotNull(value) && IsLowerThan(value.Value, min);
        public static bool IsNotNullAndLowerThanOrEqual<T>(T? value, T min) where T : struct, IComparable<T> =>
            IsNotNull(value) && IsLowerThanOrEqual(value.Value, min);
        public static bool IsNotNullAndInRange<T>(T? value, T min, T max) where T : struct, IComparable<T> =>
            IsNotNull(value) && IsInRange(value.Value, min, max);
        public static bool IsNotNullAndBetween<T>(T? value, T min, T max) where T : struct, IComparable<T> =>
            IsNotNull(value) && IsBetween(value.Value, min, max);

        public static bool IsNotNull<T>(T value) =>
            !ReferenceEquals(value, null);

        public static bool IsNotNullOrEmpty(string value) =>
            !string.IsNullOrEmpty(value);
        public static bool IsNotNullOrWhiteSpace(string value) =>
            !string.IsNullOrWhiteSpace(value);

        public static bool IsNotNullOrEmpty<T>(IEnumerable<T> collection) =>
            IsNotNull(collection) && collection.Any();
        public static bool HasAtLeast<T>(IEnumerable<T> collection, int count) =>
            collection.HasAtLeast(count);
        public static bool HasAtMost<T>(IEnumerable<T> collection, int count) =>
            collection.HasAtMost(count);

        public static bool MatchesPredicate<T>(T value, Predicate<T> predicate) =>
            predicate(value);

        public static bool IsNotNaN(double value) =>
            MatchesPredicate(value, e => !double.IsNaN(e));
    }
}
