using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
    partial class RadianAngleTests
    {
        public class ToNormalized : RadianAngleTests
        {
            public ToNormalized(TestFixture testFixture) : base(testFixture) { }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(ToNormalized), nameof(GetToNormalizedTestData))]
            public void ShouldProduceValidResultInSameUnit(RadianAngle originalRadianAngle, RadianAngle expectedRadianAngle)
            {
                // arrange
                // act
                var result = originalRadianAngle.ToNormalized();

                // assert
                result.Radians.Should().BeApproximately(expectedRadianAngle.Radians, DecimalPrecision);
            }

            private static IEnumerable<ITestDataProvider> GetToNormalizedTestData()
            {
                yield return new ToNormalizedTestData(new RadianAngle(), new RadianAngle());
                yield return new ToNormalizedTestData(new RadianAngle(0m), new RadianAngle(0m));
                yield return new ToNormalizedTestData(new RadianAngle(-0m), new RadianAngle(0m));
                yield return new ToNormalizedTestData(new RadianAngle(MathD.PI), new RadianAngle(MathD.PI));
                yield return new ToNormalizedTestData(new RadianAngle(-MathD.PI), new RadianAngle(-MathD.PI));
                yield return new ToNormalizedTestData(new RadianAngle(MathD.PI * 1.9999m), new RadianAngle(MathD.PI * 1.9999m));
                yield return new ToNormalizedTestData(new RadianAngle(-MathD.PI * 1.9999m), new RadianAngle(-MathD.PI * 1.9999m));
                yield return new ToNormalizedTestData(new RadianAngle(2m * MathD.PI), new RadianAngle(0m));
                yield return new ToNormalizedTestData(new RadianAngle(-2m * MathD.PI), new RadianAngle(0m));
                yield return new ToNormalizedTestData(new RadianAngle(MathD.PI * 73m), new RadianAngle(MathD.PI));
                yield return new ToNormalizedTestData(new RadianAngle(-MathD.PI * 73m), new RadianAngle(-MathD.PI));
            }

            class ToNormalizedTestData : ITestDataProvider
            {
                public ToNormalizedTestData(RadianAngle originalRadianAngle, RadianAngle expectedRadianAngle)
                {
                    OriginalRadianAngle = originalRadianAngle;
                    ExpectedRadianAngle = expectedRadianAngle;
                }

                public RadianAngle OriginalRadianAngle { get; }
                public RadianAngle ExpectedRadianAngle { get; }

                public object[] SerializeTestData() =>
                    new[] { (object)OriginalRadianAngle, ExpectedRadianAngle };
            }
        }
    }
}
