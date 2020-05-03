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
        public class AverageAngles : AngularEnumerableExtensionsTests
        {
            public AverageAngles(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Angle> source = null;

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
                var source = Enumerable.Empty<Angle>();

                // act
                Action average = () => AngularEnumerableExtensions.Average(source);

                // assert
                average.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Angle>(3);

                double expectedResultInTurns = source.Average(e => e.Turns);
                var expectedResultUnit = source.First().Unit;
                var expectedResult = new Angle(expectedResultInTurns).Convert(expectedResultUnit);

                // act
                var result = AngularEnumerableExtensions.Average(source);

                // assert
                result.Turns.Should().Be(expectedResult.Turns);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
