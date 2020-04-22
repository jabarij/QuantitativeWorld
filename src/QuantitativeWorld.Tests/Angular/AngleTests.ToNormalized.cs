using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
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
                result.Turns.Should().Be(expectedAngle.Turns);
                result.Value.Should().Be(expectedAngle.Value);
                result.Unit.Should().Be(expectedAngle.Unit);
            }

            private static IEnumerable<ITestDataProvider> GetToNormalizedTestData()
            {
                yield return new ToNormalizedTestData(new Angle(), new Angle());
                var units = AngleUnit.GetPredefinedUnits();
                foreach (var unit in units)
                {
                    yield return new ToNormalizedTestData(new Angle(0d, unit), new Angle(0d, unit));
                    yield return new ToNormalizedTestData(new Angle(-0d, unit), new Angle(0d, unit));
                    yield return new ToNormalizedTestData(new Angle(unit.UnitsPerTurn * 0.5d, unit), new Angle(unit.UnitsPerTurn * 0.5d, unit));
                    yield return new ToNormalizedTestData(new Angle(-unit.UnitsPerTurn * 0.5d, unit), new Angle(-unit.UnitsPerTurn * 0.5d, unit));
                    yield return new ToNormalizedTestData(new Angle(unit.UnitsPerTurn * 0.9999d, unit), new Angle(unit.UnitsPerTurn * 0.9999d, unit));
                    yield return new ToNormalizedTestData(new Angle(-unit.UnitsPerTurn * 0.9999d, unit), new Angle(-unit.UnitsPerTurn * 0.9999d, unit));
                    yield return new ToNormalizedTestData(new Angle(unit.UnitsPerTurn, unit), new Angle(0d, unit));
                    yield return new ToNormalizedTestData(new Angle(-unit.UnitsPerTurn, unit), new Angle(0d, unit));
                    yield return new ToNormalizedTestData(new Angle(unit.UnitsPerTurn * 73d + unit.UnitsPerTurn * 0.5d, unit), new Angle(unit.UnitsPerTurn * 0.5d, unit));
                    yield return new ToNormalizedTestData(new Angle(-unit.UnitsPerTurn * 73d - unit.UnitsPerTurn * 0.5d, unit), new Angle(-unit.UnitsPerTurn * 0.5d, unit));
                }
            }

            class ToNormalizedTestData : ITestDataProvider
            {
                public ToNormalizedTestData(Angle originalAngle, Angle expectedAngle)
                {
                    OriginalAngle = originalAngle;
                    ExpectedAngle = expectedAngle;
                }

                public Angle OriginalAngle { get; }
                public Angle ExpectedAngle { get; }

                public object[] SerializeTestData() =>
                    new[] { (object)OriginalAngle, ExpectedAngle };
            }
        }
    }
}
