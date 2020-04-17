using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
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
                decimal turns = Fixture.Create<decimal>();

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
                var angle = new Angle(testData.Value, testData.Unit);

                // assert
                angle.Turns.Should().BeApproximately(testData.ExpectedTurns, DecimalPrecision);
                angle.Value.Should().Be(testData.Value);
                angle.Unit.Should().Be(testData.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                yield return new ConstructorForValueAndUnitTestData(1m, AngleUnit.Turn, 1m);
                yield return new ConstructorForValueAndUnitTestData((decimal)Math.PI, AngleUnit.Radian, 1m);
                yield return new ConstructorForValueAndUnitTestData(360m, AngleUnit.Degree, 1m);
                yield return new ConstructorForValueAndUnitTestData(21600m, AngleUnit.Arcminute, 1m);
                yield return new ConstructorForValueAndUnitTestData(1296000m, AngleUnit.Arcseconds, 1m);
                yield return new ConstructorForValueAndUnitTestData(400m, AngleUnit.Gradian, 1m);
                yield return new ConstructorForValueAndUnitTestData(6400m, AngleUnit.NATOMil, 1m);
            }
            public class ConstructorForValueAndUnitTestData
            {
                public ConstructorForValueAndUnitTestData(decimal value, AngleUnit unit, decimal expectedTurns)
                {
                    Value = value;
                    Unit = unit;
                    ExpectedTurns = expectedTurns;
                }

                public decimal Value { get; }
                public AngleUnit Unit { get; }
                public decimal ExpectedTurns { get; }
            }
        }
    }
}
