using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.TestAbstractions
{
    public static class FixtureExtensions
    {
        private static readonly Random _randomGenerator = new Random(Guid.NewGuid().ToByteArray()[0]);
        private static readonly decimal _decimalEpsilon = new decimal(1, 0, 0, false, 28);

        public static int CreateBetween(this IFixture fixture, int min, int max) =>
            _randomGenerator.Next(min + 1, max);
        public static int CreateInRange(this IFixture fixture, int min, int max) =>
            _randomGenerator.Next(min, max);
        public static int CreateGreaterThan(this IFixture fixture, int min) =>
            CreateBetween(fixture, min, int.MaxValue);
        public static int CreateGreaterThanOrEqual(this IFixture fixture, int min) =>
            CreateInRange(fixture, min, int.MaxValue);

        public static decimal CreateInRange(this IFixture fixture, decimal min, decimal max)
        {
            decimal seed = new decimal(_randomGenerator.Next(), _randomGenerator.Next(), _randomGenerator.Next(), false, 28);
            if (Math.Sign(min) == Math.Sign(max) || min == 0 || max == 0)
                return decimal.Remainder(seed, max - min) + min;

            bool getFromNegativeRange = (double)min + _randomGenerator.NextDouble() * ((double)max - (double)min) < 0;
            return
                getFromNegativeRange
                ? decimal.Remainder(seed, -min) + min
                : decimal.Remainder(seed, max);
        }
        public static decimal CreateGreaterThanOrEqual(this IFixture fixture, decimal min)
        {
            if (min == decimal.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(min), "Cannot generate decimal number greater than decimal.MaxValue.");

            return CreateInRange(fixture, min, int.MaxValue);
        }
        public static decimal CreateGreaterThan(this IFixture fixture, decimal min)
        {
            if (min == decimal.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(min), "Cannot generate decimal number greater than decimal.MaxValue.");

            decimal result = CreateInRange(fixture, min, int.MaxValue);
            return
                result == min
                ? result + _decimalEpsilon
                : result;
        }
        public static decimal CreateLowerThan(this IFixture fixture, decimal max)
        {
            if (max == decimal.MinValue)
                throw new ArgumentOutOfRangeException(nameof(max), "Cannot generate decimal number lower than decimal.MinValue.");

            decimal result = CreateInRange(fixture, int.MinValue, max);
            return
                result == max
                ? result - _decimalEpsilon
                : result;
        }
        public static decimal CreateNonZeroDecimal(this IFixture fixture) =>
            fixture.Create<bool>()
            ? fixture.CreateGreaterThan(0m)
            : fixture.CreateLowerThan(0m);

        public static double CreateInRange(this IFixture fixture, double min, double max) =>
            (double)(min + _randomGenerator.NextDouble() * (max - min));
        public static double CreateGreaterThanOrEqual(this IFixture fixture, double min)
        {
            if (min == double.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(min), "Cannot generate double number greater than double.MaxValue.");

            return CreateInRange(fixture, min, double.MaxValue);
        }
        public static double CreateGreaterThan(this IFixture fixture, double min)
        {
            if (min == double.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(min), "Cannot generate double number greater than double.MaxValue.");

            double result = CreateInRange(fixture, min, double.MaxValue);
            return
                result == min
                ? result + double.Epsilon
                : result;
        }
        public static double CreateLowerThan(this IFixture fixture, double max)
        {
            if (max == double.MinValue)
                throw new ArgumentOutOfRangeException(nameof(max), "Cannot generate double number lower than double.MinValue.");

            double result = CreateInRange(fixture, double.MinValue, max);
            return
                result == max
                ? result - double.Epsilon
                : result;
        }
        public static double CreateNonZeroDouble(this IFixture fixture) =>
            fixture.Create<bool>()
            ? fixture.CreateGreaterThan(0d)
            : fixture.CreateLowerThan(0d);

        public static float CreateInRange(this IFixture fixture, float min, float max) =>
            (float)(min + _randomGenerator.NextDouble() * (max - min));
        public static float CreateGreaterThanOrEqual(this IFixture fixture, float min)
        {
            if (min == float.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(min), "Cannot generate float number greater than float.MaxValue.");

            return CreateInRange(fixture, min, float.MaxValue);
        }
        public static float CreateGreaterThan(this IFixture fixture, float min)
        {
            if (min == float.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(min), "Cannot generate float number greater than float.MaxValue.");

            float result = CreateInRange(fixture, min, float.MaxValue);
            return
                result == min
                ? result + float.Epsilon
                : result;
        }
        public static float CreateLowerThan(this IFixture fixture, float max)
        {
            if (max == float.MinValue)
                throw new ArgumentOutOfRangeException(nameof(max), "Cannot generate float number lower than float.MinValue.");

            float result = CreateInRange(fixture, float.MinValue, max);
            return
                result == max
                ? result - float.Epsilon
                : result;
        }
        public static float CreateNonZeroFloat(this IFixture fixture) =>
            fixture.Create<bool>()
            ? fixture.CreateGreaterThan(0f)
            : fixture.CreateLowerThan(0f);

        public static TValue CreateFromSet<TValue>(this IFixture fixture, IEnumerable<TValue> values)
        {
            var valuesList = values.ToList();
            return valuesList[_randomGenerator.Next(0, valuesList.Count)];
        }
    }
}
