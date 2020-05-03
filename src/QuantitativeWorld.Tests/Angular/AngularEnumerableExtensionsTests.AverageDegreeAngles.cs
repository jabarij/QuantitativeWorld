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
        public class AverageDegreeAngles : AngularEnumerableExtensionsTests
        {
            public AverageDegreeAngles(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<DegreeAngle> source = null;

                // act
                Action average = () => AngularEnumerableExtensions.Average(source);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<DegreeAngle>();

                // act
                Action average = () => AngularEnumerableExtensions.Average(source);

                // assert
                average.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<DegreeAngle>(3);

                double expectedResultInTotalSeconds = source.Average(e => e.TotalSeconds);
                var expectedResult = new DegreeAngle(expectedResultInTotalSeconds);

                // act
                var result = AngularEnumerableExtensions.Average(source);

                // assert
                result.TotalSeconds.Should().Be(expectedResult.TotalSeconds);
            }
        }
    }
}
