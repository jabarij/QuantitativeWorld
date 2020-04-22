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
                double turns = Fixture.Create<double>();

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
                angle.Turns.Should().BeApproximately(testData.ExpectedTurns, DoublePrecision);
                angle.Value.Should().Be(testData.Value);
                angle.Unit.Should().Be(testData.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                yield return new ConstructorForValueAndUnitTestData(1d, AngleUnit.Turn, 1d);
                yield return new ConstructorForValueAndUnitTestData((double)System.Math.PI, AngleUnit.Radian, 1d);
                yield return new ConstructorForValueAndUnitTestData(360d, AngleUnit.Degree, 1d);
                yield return new ConstructorForValueAndUnitTestData(21600d, AngleUnit.Arcminute, 1d);
                yield return new ConstructorForValueAndUnitTestData(1296000d, AngleUnit.Arcsecond, 1d);
                yield return new ConstructorForValueAndUnitTestData(400d, AngleUnit.Gradian, 1d);
                yield return new ConstructorForValueAndUnitTestData(6400d, AngleUnit.NATOMil, 1d);
            }
            public class ConstructorForValueAndUnitTestData
            {
                public ConstructorForValueAndUnitTestData(double value, AngleUnit unit, double expectedTurns)
                {
                    Value = value;
                    Unit = unit;
                    ExpectedTurns = expectedTurns;
                }

                public double Value { get; }
                public AngleUnit Unit { get; }
                public double ExpectedTurns { get; }
            }
        }
    }
}
