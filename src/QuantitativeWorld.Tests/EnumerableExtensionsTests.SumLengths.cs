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
        public class SumLengths : EnumerableExtensionsTests
        {
            public SumLengths(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Length> lengths = null;

                // act
                Action sum = () => EnumerableExtensions.Sum(lengths);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldReturnDefaultLength()
            {
                // arrange
                var lengths = Enumerable.Empty<Length>();

                // act
                var result = EnumerableExtensions.Sum(lengths);

                // assert
                result.Should().Be(default(Length));
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var lengths = Fixture.CreateMany<Length>(3);
                double expectedResultInMetres = lengths.Sum(e => e.Metres);
                var expectedResultUnit = lengths.First().Unit;
                var expectedResult = new Length(expectedResultInMetres).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Sum(lengths);

                // assert
                result.Metres.Should().Be(expectedResult.Metres);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
