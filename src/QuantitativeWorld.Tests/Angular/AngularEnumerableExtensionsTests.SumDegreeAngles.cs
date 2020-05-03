using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
    partial class AngularEnumerableExtensionsTests
    {
        public class SumDegreeAngles : AngularEnumerableExtensionsTests
        {
            public SumDegreeAngles(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<DegreeAngle> degreeAngles = null;

                // act
                Action sum = () => AngularEnumerableExtensions.Sum(degreeAngles);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldReturnDefaultDegreeAngle()
            {
                // arrange
                var degreeAngles = Enumerable.Empty<DegreeAngle>();

                // act
                var result = AngularEnumerableExtensions.Sum(degreeAngles);

                // assert
                result.Should().Be(default(DegreeAngle));
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var degreeAngles = Fixture.CreateMany<DegreeAngle>(3);
                double expectedResultInSeconds = degreeAngles.Sum(e => e.TotalSeconds);
                var expectedResult = new DegreeAngle(expectedResultInSeconds);

                // act
                var result = AngularEnumerableExtensions.Sum(degreeAngles);

                // assert
                result.TotalSeconds.Should().Be(expectedResult.TotalSeconds);
            }
        }
    }
}
