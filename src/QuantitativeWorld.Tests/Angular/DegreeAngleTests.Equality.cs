using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
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
                var zeroDegreeAngle = new DegreeAngle(0d);

                // act
                // assert
                zeroDegreeAngle.Equals(defaultDegreeAngle).Should().BeTrue(because: "'new DegreeAngle(0d)' should be equal 'default(DegreeAngle)'");
                defaultDegreeAngle.Equals(zeroDegreeAngle).Should().BeTrue(because: "'default(DegreeAngle)' should be equal 'new DegreeAngle(0d)'");
            }

            [Fact]
            public void DegreeAngleCreateUtinsParamlessConstructor_ShouldBeEqualToZeroDegrees()
            {
                // arrange
                var zeroDegreeAngle = new DegreeAngle(0d);
                var paramlessConstructedDegreeAngle = new DegreeAngle();

                // act
                // assert
                zeroDegreeAngle.Equals(paramlessConstructedDegreeAngle).Should().BeTrue(because: "'new DegreeAngle(0d)' should be equal 'new DegreeAngle()'");
                paramlessConstructedDegreeAngle.Equals(zeroDegreeAngle).Should().BeTrue(because: "'new DegreeAngle()' should be equal 'new DegreeAngle(0d)'");
            }
        }
    }
}
