using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class LengthTests
    {
        public class Convert : LengthTests
        {
            public Convert(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Convert), nameof(GetConvertTestData))]
            public void ShouldConvertToExpectedValue(Length originalLength, LengthUnit targetUnit, Length expectedLength)
            {
                // arrange
                // act
                var actualLength = originalLength.Convert(targetUnit);

                // assert
                actualLength.Metres.Should().BeApproximately(expectedLength.Metres, DoublePrecision);
                actualLength.Value.Should().BeApproximately(expectedLength.Value, DoublePrecision);
                actualLength.Unit.Should().Be(targetUnit);
            }

            [Fact]
            public void MultipleSerialConversion_ShouldHaveSameValueAtTheEnd()
            {
                // arrange
                var units = new List<LengthUnit>
                {
                    LengthUnit.Metre,
                    LengthUnit.Millimetre,
                    LengthUnit.Kilometre,
                    LengthUnit.Foot,
                    LengthUnit.Inch,
                    LengthUnit.Metre
                };
                var initialLength = new Length(1234.5678d, units.First());
                Length? finalLength = null;

                // act
                units.ForEach(u => finalLength = (finalLength ?? initialLength).Convert(u));

                // assert
                finalLength.Should().Be(initialLength);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                yield return new ConvertTestData(new Length(12.3456d, LengthUnit.Metre), LengthUnit.Decimetre, new Length(123.456d, LengthUnit.Decimetre));
                yield return new ConvertTestData(new Length(1234.56d, LengthUnit.Decimetre), LengthUnit.Metre, new Length(123.456d, LengthUnit.Metre));

                yield return new ConvertTestData(new Length(123.456d, LengthUnit.Metre), LengthUnit.Kilometre, new Length(0.123456d, LengthUnit.Kilometre));
                yield return new ConvertTestData(new Length(0.123456d, LengthUnit.Kilometre), LengthUnit.Metre, new Length(123.456d, LengthUnit.Metre));

                yield return new ConvertTestData(new Length(37.6293888d, LengthUnit.Metre), LengthUnit.Foot, new Length(123.456d, LengthUnit.Foot));
                yield return new ConvertTestData(new Length(123.456d, LengthUnit.Foot), LengthUnit.Metre, new Length(37.6293888d, LengthUnit.Metre));
            }

            class ConvertTestData : ITestDataProvider
            {
                public ConvertTestData(Length originalLength, LengthUnit targetUnit, Length expectedLength)
                {
                    OriginalLength = originalLength;
                    TargetUnit = targetUnit;
                    ExpectedLength = expectedLength;
                }

                public Length OriginalLength { get; }
                public LengthUnit TargetUnit { get; }
                public Length ExpectedLength { get; }

                public object[] SerializeTestData() =>
                    new[] { (object)OriginalLength, TargetUnit, ExpectedLength };
            }
        }
    }
}
