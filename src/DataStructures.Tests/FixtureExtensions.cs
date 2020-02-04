using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.Tests
{
    static class FixtureExtensions
    {
        private static readonly Random _randomGenerator = new Random(Guid.NewGuid().ToByteArray()[0]);

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

            return CreateInRange(fixture, min, decimal.MaxValue);
        }
        public static decimal CreateGreaterThan(this IFixture fixture, decimal min)
        {
            if (min == decimal.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(min), "Cannot generate decimal number greater than decimal.MaxValue.");

            decimal result = CreateInRange(fixture, min, decimal.MaxValue);
            return
                result == min
                ? result + _decimalEpsilon
                : result;
        }
        public static decimal CreateLowerThan(this IFixture fixture, decimal max)
        {
            if (max == decimal.MinValue)
                throw new ArgumentOutOfRangeException(nameof(max), "Cannot generate decimal number lower than decimal.MinValue.");

            decimal result = CreateInRange(fixture, decimal.MinValue, max);
            return
                result == max
                ? result - _decimalEpsilon
                : result;
        }
        public static decimal CreateNonZeroDecimal(this IFixture fixture) =>
            fixture.Create<bool>()
            ? fixture.CreateGreaterThan(0m)
            : fixture.CreateLowerThan(0m);

        private static readonly decimal _decimalEpsilon = new decimal(1, 0, 0, false, 28);

        public static TValue CreateFromSet<TValue>(this IFixture fixture, IEnumerable<TValue> values)
        {
            var valuesList = values.ToList();
            return valuesList[_randomGenerator.Next(0, valuesList.Count)];
        }
    }
}
