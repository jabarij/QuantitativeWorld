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
        public class AverageLengths : EnumerableExtensionsTests
        {
            public AverageLengths(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Length> source = null;

                // act
                Action average = () => EnumerableExtensions.Average(source);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<Length>();

                // act
                Action average = () => EnumerableExtensions.Average(source);

                // assert
                average.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Length>(3);

                decimal expectedResultInMetres = source.Average(e => e.Metres);
                var expectedResultUnit = source.First().Unit;
                var expectedResult = new Length(expectedResultInMetres).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Average(source);

                // assert
                result.Metres.Should().Be(expectedResult.Metres);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
