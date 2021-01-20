using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
#if DECIMAL
    using number = System.Decimal;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial class AngleTests
    {
        public class ToNormalized : AngleTests
        {
            public ToNormalized(TestFixture testFixture) : base(testFixture) { }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(ToNormalized), nameof(GetToNormalizedTestData))]
            public void ShouldProduceValidResultInSameUnit(Angle originalAngle, Angle expectedAngle)
            {
                // arrange
                // act
                var result = originalAngle.ToNormalized();

                // assert
                result.Turns.Should().BeApproximately(expectedAngle.Turns);
                result.Value.Should().BeApproximately(expectedAngle.Value);
                result.Unit.Should().Be(expectedAngle.Unit);
            }

            private static IEnumerable<ITestDataProvider> GetToNormalizedTestData()
            {
                yield return new ToNormalizedTestData(new Angle(), new Angle());
                var units = AngleUnit.GetPredefinedUnits();
                const number zero = (number)0m;
                const number half = (number)0.5m;
                const number almostOne = (number)0.9999m;
                const number positiveEven = (number)73m;
                foreach (var unit in units)
                {
                    yield return new ToNormalizedTestData(0m, unit, 0m, unit);
                    yield return new ToNormalizedTestData(-0m, unit, 0m, unit);
                    yield return new ToNormalizedTestData(unit.UnitsPerTurn * half, unit, unit.UnitsPerTurn * half, unit);
                    yield return new ToNormalizedTestData(-unit.UnitsPerTurn * half, unit, -unit.UnitsPerTurn * half, unit);
                    yield return new ToNormalizedTestData(unit.UnitsPerTurn * almostOne, unit, unit.UnitsPerTurn * almostOne, unit);
                    yield return new ToNormalizedTestData(-unit.UnitsPerTurn * almostOne, unit, -unit.UnitsPerTurn * almostOne, unit);
                    yield return new ToNormalizedTestData(unit.UnitsPerTurn, unit, zero, unit);
                    yield return new ToNormalizedTestData(-unit.UnitsPerTurn, unit, zero, unit);
                    yield return new ToNormalizedTestData(unit.UnitsPerTurn * positiveEven + unit.UnitsPerTurn * half, unit, unit.UnitsPerTurn * half, unit);
                    yield return new ToNormalizedTestData(-unit.UnitsPerTurn * positiveEven - unit.UnitsPerTurn * half, unit, -unit.UnitsPerTurn * half, unit);
                }
            }

            class ToNormalizedTestData : ConversionTestData<Angle>, ITestDataProvider
            {
                public ToNormalizedTestData(Angle originalValue, Angle expectedValue)
                    : base(originalValue, expectedValue) { }
                public ToNormalizedTestData(decimal originalValue, AngleUnit originalUnit, decimal expectedValue, AngleUnit expectedUnit)
                    : base(new Angle((number)originalValue, originalUnit), new Angle((number)expectedValue, expectedUnit)) { }
                public ToNormalizedTestData(double originalValue, AngleUnit originalUnit, double expectedValue, AngleUnit expectedUnit)
                    : base(new Angle((number)originalValue, originalUnit), new Angle((number)expectedValue, expectedUnit)) { }

                public object[] GetTestParameters() =>
                    new[] { (object)OriginalValue, ExpectedValue };
            }
        }
    }
}
