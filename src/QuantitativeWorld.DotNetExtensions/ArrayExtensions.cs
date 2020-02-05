using System.Collections.Generic;

namespace QuantitativeWorld.DotNetExtensions
{
    public static class ArrayExtensions
    {
        public static bool ArrayEqual<T>(this T[] array, T[] other, IEqualityComparer<T> comparer)
        {
            if (array.Length != other.Length)
                return false;

            for (int index = 0; index < array.Length; index++)
                if (!comparer.Equals(array[index], other[index]))
                    return false;

            return true;
        }

        public static bool ArrayEqual<T>(this T[] array, T[] other) =>
            ArrayEqual(array, other, EqualityComparer<T>.Default);
    }
}
