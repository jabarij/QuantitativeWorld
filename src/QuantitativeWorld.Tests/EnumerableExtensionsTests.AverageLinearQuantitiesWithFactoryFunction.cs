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
        public class AverageLinearQuantitiesWithFactoryFunction : EnumerableExtensionsTests
        {
            public AverageLinearQuantitiesWithFactoryFunction(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Power> source = null;
                Func<decimal, PowerUnit, Power> factory = PowerFactory.Create;

                // act
                Action average = () => EnumerableExtensions.Average<Power, PowerUnit>(source, factory);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void NullFactory_ShouldThrow()
            {
                // arrange
                var source = Fixture.CreateMany<Power>(3);
                Func<decimal, PowerUnit, Power> factory = null;

                // act
                Action average = () => EnumerableExtensions.Average<Power, PowerUnit>(source, factory);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("factory");
            }

            [Fact]
            public void EmptySource_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<Power>();
                Func<decimal, PowerUnit, Power> factory = PowerFactory.Create;

                // act
                Action average = () => EnumerableExtensions.Average(source, factory);

                // assert
                average.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Power>(3);
                Func<decimal, PowerUnit, Power> factory = PowerFactory.Create;

                decimal expectedResultInWatts = source.Average(e => e.Value * e.Unit.ValueInWatts);
                var expectedResultUnit = source.First().Unit;
                var expectedResult = new Power(expectedResultInWatts / expectedResultUnit.ValueInWatts, expectedResultUnit);

                // act
                var result = EnumerableExtensions.Average<Power, PowerUnit>(source, factory);

                // assert
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
