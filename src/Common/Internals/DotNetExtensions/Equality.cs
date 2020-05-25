using System;

namespace QuantitativeWorld.DotNetExtensions
{
    internal static class Equality
    {
        public static bool AreEqual<T>(T left, T right) =>
            ReferenceEquals(left, right)
            || ReferenceEquals(left, null) == ReferenceEquals(right, null)
                ? ReferenceEquals(left, null) || left.Equals(right)
                : false;

        public static bool AreEqualStructures<T>(T left, T right)
            where T : struct =>
            left.Equals(right);

        public static bool IsEqualToObject<T>(T me, object obj)
            where T : IEquatable<T> =>
            ReferenceEquals(me, obj)
            || !ReferenceEquals(me, null) && !ReferenceEquals(obj, null)
                && obj is T other
                && me.Equals(other);

        public static bool IsStructureEqualToObject<T>(T me, object obj)
            where T : struct, IEquatable<T> =>
            obj is T
            && me.Equals((T)obj);

        public static bool IsStructureGreaterThan<T>(T me, T other)
            where T : struct, IComparable<T> =>
            me.CompareTo(other) > 0;

        public static bool IsStructureLowerThan<T>(T me, T other)
            where T : struct, IComparable<T> =>
            me.CompareTo(other) < 0;

        public static bool IsStructureGreaterThanOrEqual<T>(T me, T other)
            where T : struct, IComparable<T> =>
            me.CompareTo(other) >= 0;

        public static bool IsStructureLowerThanOrEqual<T>(T me, T other)
            where T : struct, IComparable<T> =>
            me.CompareTo(other) <= 0;
    }
}
