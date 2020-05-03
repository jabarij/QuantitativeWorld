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
        public class SumAreas : EnumerableExtensionsTests
        {
            public SumAreas(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Area> areas = null;

                // act
                Action sum = () => EnumerableExtensions.Sum(areas);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldReturnDefaultArea()
            {
                // arrange
                var areas = Enumerable.Empty<Area>();

                // act
                var result = EnumerableExtensions.Sum(areas);

                // assert
                result.Should().Be(default(Area));
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var areas = Fixture.CreateMany<Area>(3);
                double expectedResultInSquareMetres = areas.Sum(e => e.SquareMetres);
                var expectedResultUnit = areas.First().Unit;
                var expectedResult = new Area(expectedResultInSquareMetres).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Sum(areas);

                // assert
                result.SquareMetres.Should().Be(expectedResult.SquareMetres);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
