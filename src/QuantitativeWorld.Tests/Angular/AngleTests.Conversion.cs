using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
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
                actualAngle.Should().Be(expectedAngle);
                actualAngle.Unit.Should().Be(targetUnit);
            }

            [Fact]
            public void MultipleSerialConversion_ShouldHaveSameValueAtTheEnd()
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
                var initialAngle = new Angle(123.45678m, units.First());
                Angle? finalAngle = null;

                // act
                units.ForEach(u => finalAngle = (finalAngle ?? initialAngle).Convert(u));

                // assert
                finalAngle.Should().Be(initialAngle);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                yield return new ConvertTestData(new Angle(0.123m, AngleUnit.Turn), AngleUnit.Radian, new Angle(0.123m * (decimal)Math.PI, AngleUnit.Radian));
                yield return new ConvertTestData(new Angle(123.456m, AngleUnit.Radian), AngleUnit.Turn, new Angle(123.456m / (decimal)Math.PI, AngleUnit.Turn));

                yield return new ConvertTestData(new Angle(0.123456m, AngleUnit.Turn), AngleUnit.Degree, new Angle(0.123456m * 360m, AngleUnit.Degree));
                yield return new ConvertTestData(new Angle(123.456m, AngleUnit.Degree), AngleUnit.Turn, new Angle(123.456m / 360m, AngleUnit.Turn));

                yield return new ConvertTestData(new Angle(0.123456m, AngleUnit.Turn), AngleUnit.Gradian, new Angle(0.123456m * 400m, AngleUnit.Gradian));
                yield return new ConvertTestData(new Angle(123.456m, AngleUnit.Gradian), AngleUnit.Turn, new Angle(123.456m / 400m, AngleUnit.Turn));
            }

            class ConvertTestData : ITestDataProvider
            {
                public ConvertTestData(Angle originalAngle, AngleUnit targetUnit, Angle expectedAngle)
                {
                    OriginalAngle = originalAngle;
                    TargetUnit = targetUnit;
                    ExpectedAngle = expectedAngle;
                }

                public Angle OriginalAngle { get; }
                public AngleUnit TargetUnit { get; }
                public Angle ExpectedAngle { get; }

                public object[] SerializeTestData() =>
                    new[] { (object)OriginalAngle, TargetUnit, ExpectedAngle };
            }
        }
    }
}
