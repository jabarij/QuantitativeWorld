using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.TestAbstractions
{
    using number = Decimal;
#else
namespace QuantitativeWorld.TestAbstractions
{
    using number = Double;
#endif

    public static class FixtureExtensions
    {
        private static readonly Random _randomGenerator = new Random(Guid.NewGuid().ToByteArray()[0]);
        private static readonly decimal _decimalEpsilon = new decimal(1, 0, 0, false, 28);
        private const double MinDouble = -1000d;
        private const double MaxDouble = 1000d;
        private const decimal MinDecimal = -1000m;
        private const decimal MaxDecimal = 1000m;

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
            min + _randomGenerator.NextDouble() * (max - min);
        public static double CreateNonNegativeDouble(this IFixture fixture) =>
            CreateInRange(fixture, 0d, MaxDouble);
        public static double CreatePositiveDouble(this IFixture fixture) =>
            CreateInRange(fixture, 1d, MaxDouble);
        public static double CreateNegativeDouble(this IFixture fixture) =>
            CreateInRange(fixture, MinDouble, -1d);
        public static double CreateNonZeroDouble(this IFixture fixture) =>
            fixture.Create<bool>()
            ? fixture.CreatePositiveDouble()
            : fixture.CreateNegativeDouble();

#if DECIMAL
        public static number CreatePositiveNumber(this IFixture fixture) =>
            CreateGreaterThan(fixture, 0m);
        public static number CreateNegativeNumber(this IFixture fixture) =>
            CreateLowerThan(fixture, 0m);
        public static number CreateNonZeroNumber(this IFixture fixture) =>
            CreateNonZeroDecimal(fixture);
#else
        public static number CreatePositiveNumber(this IFixture fixture) =>
            CreatePositiveDouble(fixture);
        public static number CreateNegativeNumber(this IFixture fixture) =>
            CreateNegativeDouble(fixture);
        public static number CreateNonZeroNumber(this IFixture fixture) =>
            CreateNonZeroDouble(fixture);
#endif

        public static TValue CreateFromSet<TValue>(this IFixture fixture, IEnumerable<TValue> values)
        {
            var valuesList = values.ToList();
            return valuesList[_randomGenerator.Next(0, valuesList.Count)];
        }
    }
}
