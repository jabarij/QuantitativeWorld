using FluentAssertions;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
    using Constants = DecimalConstants;
#else
namespace QuantitativeWorld.Tests.Angular
{
    using QuantitativeWorld.Angular;
    using QuantitativeWorld.TestAbstractions;
    using Constants = DoubleConstants;
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
