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
                result.Radians.Should().BeApproximately(expectedRadianAngle.Radians);
            }

            private static IEnumerable<ITestDataProvider> GetToNormalizedTestData()
            {
                const number zero = (number)0m;
                const number two = (number)2m;
                const number almostTwo = (number)1.9999m;
                const number positiveEven = (number)73m;
                yield return new ToNormalizedTestData(new RadianAngle(), new RadianAngle());
                yield return new ToNormalizedTestData(0m, 0m);
                yield return new ToNormalizedTestData(-0m, 0m);
                yield return new ToNormalizedTestData(Constants.PI, Constants.PI);
                yield return new ToNormalizedTestData(-Constants.PI, -Constants.PI);
                yield return new ToNormalizedTestData(almostTwo * Constants.PI, almostTwo * Constants.PI);
                yield return new ToNormalizedTestData(-almostTwo * Constants.PI, -almostTwo * Constants.PI);
                yield return new ToNormalizedTestData(two * Constants.PI, zero);
                yield return new ToNormalizedTestData(-two * Constants.PI, zero);
                yield return new ToNormalizedTestData(positiveEven * Constants.PI, Constants.PI);
                yield return new ToNormalizedTestData(-positiveEven * Constants.PI, -Constants.PI);
            }

            class ToNormalizedTestData : ConversionTestData<RadianAngle>, ITestDataProvider
            {
                public ToNormalizedTestData(RadianAngle originalValue, RadianAngle expectedValue)
                    : base(originalValue, expectedValue) { }
                public ToNormalizedTestData(decimal originalValue, decimal expectedValue)
                    : base(new RadianAngle((number)originalValue), new RadianAngle((number)expectedValue)) { }

                public object[] GetTestParameters() =>
                    new[] { (object)OriginalValue, ExpectedValue };
            }
        }
    }
}
