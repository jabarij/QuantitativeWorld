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
        public class SumAngles : AngularEnumerableExtensionsTests
        {
            public SumAngles(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Angle> angles = null;

                // act
                Action sum = () => AngularEnumerableExtensions.Sum(angles);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldReturnDefaultAngle()
            {
                // arrange
                var angles = Enumerable.Empty<Angle>();

                // act
                var result = AngularEnumerableExtensions.Sum(angles);

                // assert
                result.Should().Be(default(Angle));
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var angles = Fixture.CreateMany<Angle>(3);
                double expectedResultInTurns = angles.Sum(e => e.Turns);
                var expectedResultUnit = angles.First().Unit;
                var expectedResult = new Angle(expectedResultInTurns).Convert(expectedResultUnit);

                // act
                var result = AngularEnumerableExtensions.Sum(angles);

                // assert
                result.Turns.Should().Be(expectedResult.Turns);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
