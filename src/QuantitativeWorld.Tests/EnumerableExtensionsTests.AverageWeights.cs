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
        public class AverageWeights : EnumerableExtensionsTests
        {
            public AverageWeights(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Weight> weights = null;

                // act
                Action average = () => EnumerableExtensions.Average(weights);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldReturnDefaultWeight()
            {
                // arrange
                var weights = Enumerable.Empty<Weight>();

                // act
                var result = EnumerableExtensions.Average(weights);

                // assert
                result.Should().Be(default(Weight));
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var weights = Fixture.CreateMany<Weight>(3);
                decimal expectedResultInKilograms = weights.Average(e => e.Kilograms);
                var expectedResultUnit = weights.First().Unit;
                var expectedResult = new Weight(expectedResultInKilograms).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Average(weights);

                // assert
                result.Kilograms.Should().Be(expectedResult.Kilograms);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
