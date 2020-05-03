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
        public class AverageVolumes : EnumerableExtensionsTests
        {
            public AverageVolumes(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Volume> source = null;

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
                var source = Enumerable.Empty<Volume>();

                // act
                Action average = () => EnumerableExtensions.Average(source);


                // assert
                average.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Volume>(3);
                double expectedResultInCubicMetres = source.Average(e => e.CubicMetres);
                var expectedResultUnit = source.First().Unit;
                var expectedResult = new Volume(expectedResultInCubicMetres).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Average(source);

                // assert
                result.CubicMetres.Should().Be(expectedResult.CubicMetres);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
