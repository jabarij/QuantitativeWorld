using AutoFixture;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
    using Constants = DecimalConstants;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Tests.Angular
{
    using QuantitativeWorld.Angular;
    using QuantitativeWorld.TestAbstractions;
    using Constants = DoubleConstants;
    using number = System.Double;
#endif

    partial class AngleTests
    {
        public class Creation : AngleTests
        {
            public Creation(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ConstructorForTurns_ShouldCreateValidAngle()
            {
                // arrange
                number turns = Fixture.Create<number>();

                // act
                var angle = new Angle(turns);

                // assert
                angle.Turns.Should().Be(turns);
                angle.Value.Should().Be(turns);
                angle.Unit.Should().Be(AngleUnit.Turn);
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Creation), nameof(GetConstructorForValueAndUnitTestData))]
            public void ConstructorForValueAndUnit_ShouldCreateValidAngle(ConstructorForValueAndUnitTestData testData)
            {
                // arrange
                // act
                var angle = testData.OriginalValue;

                // assert
                angle.Turns.Should().BeApproximately(testData.ExpectedValue.Turns);
                angle.Value.Should().BeApproximately(testData.OriginalValue.Value);
                angle.Unit.Should().Be(testData.OriginalValue.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                const number one = (number)1m;
                yield return new ConstructorForValueAndUnitTestData(Constants.One, AngleUnit.Turn, one);
                yield return new ConstructorForValueAndUnitTestData(Constants.PI, AngleUnit.Radian, one);
                yield return new ConstructorForValueAndUnitTestData(360m, AngleUnit.Degree, 1m);
                yield return new ConstructorForValueAndUnitTestData(21600m, AngleUnit.Arcminute, 1m);
                yield return new ConstructorForValueAndUnitTestData(1296000m, AngleUnit.Arcsecond, 1m);
                yield return new ConstructorForValueAndUnitTestData(400m, AngleUnit.Gradian, 1m);
                yield return new ConstructorForValueAndUnitTestData(6400m, AngleUnit.NATOMil, 1m);
            }
            public class ConstructorForValueAndUnitTestData : ConversionTestData<Angle>
            {
                public ConstructorForValueAndUnitTestData(double originalValue, AngleUnit unit, double expectedTurns)
                    : base(new Angle((number)originalValue, unit), new Angle((number)expectedTurns)) { }
                public ConstructorForValueAndUnitTestData(decimal originalValue, AngleUnit unit, decimal expectedTurns)
                    : base(new Angle((number)originalValue, unit), new Angle((number)expectedTurns)) { }
            }
        }
    }
}
