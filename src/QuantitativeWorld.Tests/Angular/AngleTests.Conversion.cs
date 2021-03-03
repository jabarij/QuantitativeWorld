using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = Decimal;
#else
namespace QuantitativeWorld.Tests.Angular
{
    using QuantitativeWorld.Angular;
    using QuantitativeWorld.TestAbstractions;
    using number = Double;
#endif

    partial class AngleTests
    {
        public class Convert : AngleTests
        {
            public Convert(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Convert), nameof(GetConvertTestData))]
            public void ShouldConvertToExpectedValue(Angle originalAngle, AngleUnit targetUnit, Angle expectedAngle)
            {
                // arrange
                // act
                var actualAngle = originalAngle.Convert(targetUnit);

                // assert
                actualAngle.Value.Should().BeApproximately(expectedAngle.Value);
                actualAngle.Unit.Should().Be(targetUnit);
            }

            [Theory]
            [InlineData(123.45678d)]
            public void MultipleSerialConversion_ShouldHaveSameValueAtTheEnd(number value)
            {
                // arrange
                var units = new List<AngleUnit>
                {
                    AngleUnit.Turn,
                    AngleUnit.Radian,
                    AngleUnit.Degree,
                    AngleUnit.Gradian,
                    AngleUnit.Turn
                };
                var initialAngle = new Angle(value, units.First());
                Angle? finalAngle = null;

                // act
                units.ForEach(u => finalAngle = (finalAngle ?? initialAngle).Convert(u));

                // assert
                finalAngle.Should().Be(initialAngle);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                yield return new ConvertTestData(0.123d, AngleUnit.Turn, AngleUnit.Radian, 0.123d * Math.PI, AngleUnit.Radian);
                yield return new ConvertTestData(123.456d, AngleUnit.Radian, AngleUnit.Turn, 123.456d / Math.PI, AngleUnit.Turn);

                yield return new ConvertTestData(0.123456d, AngleUnit.Turn, AngleUnit.Degree, 0.123456d * 360d, AngleUnit.Degree);
                yield return new ConvertTestData(123.456d, AngleUnit.Degree, AngleUnit.Turn, 123.456d / 360d, AngleUnit.Turn);

                yield return new ConvertTestData(0.123456d, AngleUnit.Turn, AngleUnit.Gradian, 0.123456d * 400d, AngleUnit.Gradian);
                yield return new ConvertTestData(123.456d, AngleUnit.Gradian, AngleUnit.Turn, 123.456d / 400d, AngleUnit.Turn);
            }

            class ConvertTestData : ITestDataProvider
            {
                public ConvertTestData(Angle originalAngle, AngleUnit targetUnit, Angle expectedAngle)
                {
                    OriginalAngle = originalAngle;
                    TargetUnit = targetUnit;
                    ExpectedAngle = expectedAngle;
                }
                public ConvertTestData(double originalValue, AngleUnit originalUnit, AngleUnit targetUnit, double expectedValue, AngleUnit expectedUnit)
                    : this(new Angle((number)originalValue, originalUnit), targetUnit, new Angle((number)expectedValue, expectedUnit)) { }
                public ConvertTestData(decimal originalValue, AngleUnit originalUnit, AngleUnit targetUnit, decimal expectedValue, AngleUnit expectedUnit)
                    : this(new Angle((number)originalValue, originalUnit), targetUnit, new Angle((number)expectedValue, expectedUnit)) { }

                public Angle OriginalAngle { get; }
                public AngleUnit TargetUnit { get; }
                public Angle ExpectedAngle { get; }

                public object[] GetTestParameters() =>
                    new[] { (object)OriginalAngle, TargetUnit, ExpectedAngle };
            }
        }
    }
}
