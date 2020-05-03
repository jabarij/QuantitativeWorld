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
        public class AverageAreas : EnumerableExtensionsTests
        {
            public AverageAreas(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Area> source = null;

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
                var source = Enumerable.Empty<Area>();

                // act
                Action average = () => EnumerableExtensions.Average(source);

                // assert
                average.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Area>(3);

                double expectedResultInSquareMetres = source.Average(e => e.SquareMetres);
                var expectedResultUnit = source.First().Unit;
                var expectedResult = new Area(expectedResultInSquareMetres).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Average(source);

                // assert
                result.SquareMetres.Should().Be(expectedResult.SquareMetres);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
