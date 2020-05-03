using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class EnumerableExtensionsTests
    {
        public class SumPowers : EnumerableExtensionsTests
        {
            public SumPowers(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Power> powers = null;

                // act
                Action sum = () => EnumerableExtensions.Sum(powers);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldReturnDefaultPower()
            {
                // arrange
                var powers = Enumerable.Empty<Power>();

                // act
                var result = EnumerableExtensions.Sum(powers);

                // assert
                result.Should().Be(default(Power));
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var powers = Fixture.CreateMany<Power>(3);
                double expectedResultInWatts = powers.Sum(e => e.Watts);
                var expectedResultUnit = powers.First().Unit;
                var expectedResult = new Power(expectedResultInWatts).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Sum(powers);

                // assert
                result.Watts.Should().Be(expectedResult.Watts);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
