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
        public class SumLinearQuantitiesWithFactoryFunction : EnumerableExtensionsTests
        {
            public SumLinearQuantitiesWithFactoryFunction(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Power> quantities = null;
                Func<decimal, PowerUnit, Power> factory = PowerFactory.Create;

                // act
                Action sum = () => EnumerableExtensions.Sum<Power, PowerUnit>(quantities, factory);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void NullFactory_ShouldThrow()
            {
                // arrange
                var quantities = Fixture.CreateMany<Power>(3);
                Func<decimal, PowerUnit, Power> factory = null;

                // act
                Action sum = () => EnumerableExtensions.Sum<Power, PowerUnit>(quantities, factory);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("factory");
            }

            [Fact]
            public void EmptySource_ShouldReturnDefaultTestQuantity()
            {
                // arrange
                var quantities = Enumerable.Empty<Power>();
                Func<decimal, PowerUnit, Power> factory = PowerFactory.Create;

                // act
                var result = EnumerableExtensions.Sum<Power, PowerUnit>(quantities, factory);

                // assert
                result.Should().Be(default(Power));
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var quantities = Fixture.CreateMany<Power>(3);
                Func<decimal, PowerUnit, Power> factory = PowerFactory.Create;

                decimal expectedResultInWatts = quantities.Sum(e => e.Value * e.Unit.ValueInWatts);
                var expectedResultUnit = quantities.First().Unit;
                var expectedResult = new Power(expectedResultInWatts / expectedResultUnit.ValueInWatts, PowerUnit.Watt);

                // act
                var result = EnumerableExtensions.Sum<Power, PowerUnit>(quantities, factory);

                // assert
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
