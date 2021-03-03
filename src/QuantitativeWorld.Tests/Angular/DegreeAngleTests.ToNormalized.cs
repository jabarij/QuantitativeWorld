using FluentAssertions;
using System.Collections.Generic;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Tests.Angular
{
    using QuantitativeWorld.Angular;
    using QuantitativeWorld.TestAbstractions;
    using number = System.Double;
#endif

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
                result.TotalSeconds.Should().BeApproximately(expectedDegreeAngle.TotalSeconds);
            }

            private static IEnumerable<ITestDataProvider> GetToNormalizedTestData()
            {
                yield return new ToNormalizedTestData(new DegreeAngle(), new DegreeAngle());
                yield return new ToNormalizedTestData(0m, 0m);
                yield return new ToNormalizedTestData(-0m, 0m);
                yield return new ToNormalizedTestData(180m * 60m * 60m, 180m * 60m * 60m);
                yield return new ToNormalizedTestData(-180m * 60m * 60m, -180m * 60m * 60m);
                yield return new ToNormalizedTestData(0.9999m * 360m * 60m * 60m, 0.9999m * 360m * 60m * 60m);
                yield return new ToNormalizedTestData(-0.9999m * 360m * 60m * 60m, -0.9999m * 360m * 60m * 60m);
                yield return new ToNormalizedTestData(360m * 60m * 60m, 0m);
                yield return new ToNormalizedTestData(-360m * 60m * 60m, 0m);
                yield return new ToNormalizedTestData(73.5m * 360m * 60m * 60m, 180m * 60m * 60m);
                yield return new ToNormalizedTestData(-73.5m * 360m * 60m * 60m, -180m * 60m * 60m);
            }

            class ToNormalizedTestData : ConversionTestData<DegreeAngle>, ITestDataProvider
            {
                public ToNormalizedTestData(DegreeAngle originalValue, DegreeAngle expectedValue)
                    : base(originalValue, expectedValue) { }
                public ToNormalizedTestData(decimal originalTotalSeconds, decimal expectedTotalSeconds)
                    : base(new DegreeAngle((number)originalTotalSeconds), new DegreeAngle((number)expectedTotalSeconds)) { }

                public object[] GetTestParameters() =>
                    new[] { (object)OriginalValue, ExpectedValue };
            }
        }
    }
}
