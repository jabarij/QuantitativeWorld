using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System;
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
                result.Radians.Should().BeApproximately(expectedRadianAngle.Radians, DoublePrecision);
            }

            private static IEnumerable<ITestDataProvider> GetToNormalizedTestData()
            {
                yield return new ToNormalizedTestData(new RadianAngle(), new RadianAngle());
                yield return new ToNormalizedTestData(new RadianAngle(0d), new RadianAngle(0d));
                yield return new ToNormalizedTestData(new RadianAngle(-0d), new RadianAngle(0d));
                yield return new ToNormalizedTestData(new RadianAngle(System.Math.PI), new RadianAngle(System.Math.PI));
                yield return new ToNormalizedTestData(new RadianAngle(-System.Math.PI), new RadianAngle(-System.Math.PI));
                yield return new ToNormalizedTestData(new RadianAngle(1.9999d * System.Math.PI), new RadianAngle(1.9999d * System.Math.PI));
                yield return new ToNormalizedTestData(new RadianAngle(-1.9999d * System.Math.PI), new RadianAngle(-1.9999d * System.Math.PI));
                yield return new ToNormalizedTestData(new RadianAngle(2d * System.Math.PI), new RadianAngle(0d));
                yield return new ToNormalizedTestData(new RadianAngle(-2d * System.Math.PI), new RadianAngle(0d));
                yield return new ToNormalizedTestData(new RadianAngle(73d * System.Math.PI), new RadianAngle(System.Math.PI));
                yield return new ToNormalizedTestData(new RadianAngle(-73d * System.Math.PI), new RadianAngle(-System.Math.PI));
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
