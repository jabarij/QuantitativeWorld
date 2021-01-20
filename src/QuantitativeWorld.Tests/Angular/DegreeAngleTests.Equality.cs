using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial class DegreeAngleTests
    {
        public class Equality : DegreeAngleTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultDegreeAngle_ShouldBeEqualToZeroDegrees()
            {
                // arrange
                var defaultDegreeAngle = default(DegreeAngle);
                var zeroDegreeAngle = new DegreeAngle(Constants.Zero);

                // act
                // assert
                zeroDegreeAngle.Equals(defaultDegreeAngle).Should().BeTrue(because: "'new DegreeAngle(Constants.Zero)' should be equal 'default(DegreeAngle)'");
                defaultDegreeAngle.Equals(zeroDegreeAngle).Should().BeTrue(because: "'default(DegreeAngle)' should be equal 'new DegreeAngle(Constants.Zero)'");
            }

            [Fact]
            public void DegreeAngleCreateUsingParamlessConstructor_ShouldBeEqualToZeroDegrees()
            {
                // arrange
                var zeroDegreeAngle = new DegreeAngle(Constants.Zero);
                var paramlessConstructedDegreeAngle = new DegreeAngle();

                // act
                // assert
                zeroDegreeAngle.Equals(paramlessConstructedDegreeAngle).Should().BeTrue(because: "'new DegreeAngle(Constants.Zero)' should be equal 'new DegreeAngle()'");
                paramlessConstructedDegreeAngle.Equals(zeroDegreeAngle).Should().BeTrue(because: "'new DegreeAngle()' should be equal 'new DegreeAngle(Constants.Zero)'");
            }
        }
    }
}
