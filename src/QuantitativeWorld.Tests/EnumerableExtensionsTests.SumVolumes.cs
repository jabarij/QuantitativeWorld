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
        public class SumVolumes : EnumerableExtensionsTests
        {
            public SumVolumes(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Volume> areas = null;

                // act
                Action sum = () => EnumerableExtensions.Sum(areas);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldReturnDefaultVolume()
            {
                // arrange
                var areas = Enumerable.Empty<Volume>();

                // act
                var result = EnumerableExtensions.Sum(areas);

                // assert
                result.Should().Be(default(Volume));
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var areas = Fixture.CreateMany<Volume>(3);
                double expectedResultInCubicMetres = areas.Sum(e => e.CubicMetres);
                var expectedResultUnit = areas.First().Unit;
                var expectedResult = new Volume(expectedResultInCubicMetres).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Sum(areas);

                // assert
                result.CubicMetres.Should().Be(expectedResult.CubicMetres);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
