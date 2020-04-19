using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
    partial class DegreeAngleTests
    {
        public class ToNormalized : DegreeAngleTests
        {
            public ToNormalized(TestFixture testFixture) : base(testFixture) { }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(ToNormalized), nameof(GetToNormalizedTestData))]
            public void ShouldProduceValidResultInSameUnit(DegreeAngle originalDegreeAngle, DegreeAngle expectedDegreeAngle)
            {
                // arrange
                // act
                var result = originalDegreeAngle.ToNormalized();

                // assert
                result.TotalSeconds.Should().BeApproximately(expectedDegreeAngle.TotalSeconds, DoublePrecision);
            }

            private static IEnumerable<ITestDataProvider> GetToNormalizedTestData()
            {
                yield return new ToNormalizedTestData(new DegreeAngle(), new DegreeAngle());
                yield return new ToNormalizedTestData(new DegreeAngle(0d), new DegreeAngle(0d));
                yield return new ToNormalizedTestData(new DegreeAngle(-0d), new DegreeAngle(0d));
                yield return new ToNormalizedTestData(new DegreeAngle(180d * 60d * 60d), new DegreeAngle(180d * 60d * 60d));
                yield return new ToNormalizedTestData(new DegreeAngle(-180d * 60d * 60d), new DegreeAngle(-180d * 60d * 60d));
                yield return new ToNormalizedTestData(new DegreeAngle(0.9999d * 360d * 60d * 60d), new DegreeAngle(0.9999d * 360d * 60d * 60d));
                yield return new ToNormalizedTestData(new DegreeAngle(-0.9999d * 360d * 60d * 60d), new DegreeAngle(-0.9999d * 360d * 60d * 60d));
                yield return new ToNormalizedTestData(new DegreeAngle(360d * 60d * 60d), new DegreeAngle(0d));
                yield return new ToNormalizedTestData(new DegreeAngle(-360d * 60d * 60d), new DegreeAngle(0d));
                yield return new ToNormalizedTestData(new DegreeAngle(73.5d * 360d * 60d * 60d), new DegreeAngle(180d * 60d * 60d));
                yield return new ToNormalizedTestData(new DegreeAngle(-73.5d * 360d * 60d * 60d), new DegreeAngle(-180d * 60d * 60d));
            }

            class ToNormalizedTestData : ITestDataProvider
            {
                public ToNormalizedTestData(DegreeAngle originalDegreeAngle, DegreeAngle expectedDegreeAngle)
                {
                    OriginalDegreeAngle = originalDegreeAngle;
                    ExpectedDegreeAngle = expectedDegreeAngle;
                }

                public DegreeAngle OriginalDegreeAngle { get; }
                public DegreeAngle ExpectedDegreeAngle { get; }

                public object[] SerializeTestData() =>
                    new[] { (object)OriginalDegreeAngle, ExpectedDegreeAngle };
            }
        }
    }
}
