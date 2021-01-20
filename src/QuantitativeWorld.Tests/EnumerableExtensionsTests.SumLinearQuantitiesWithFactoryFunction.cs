using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial class EnumerableExtensionsTests
    {
        public class SumLinearQuantitiesWithFactoryFunction : EnumerableExtensionsTests
        {
            public SumLinearQuantitiesWithFactoryFunction(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<SomeQuantity> quantities = null;
                Func<number, SomeUnit, SomeQuantity> factory = SomeQuantityFactory.Create;

                // act
                Action sum = () => EnumerableExtensions.Sum<SomeQuantity, SomeUnit>(quantities, factory);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void NullFactory_ShouldThrow()
            {
                // arrange
                var quantities = Fixture.CreateMany<SomeQuantity>(3);
                Func<number, SomeUnit, SomeQuantity> factory = null;

                // act
                Action sum = () => EnumerableExtensions.Sum<SomeQuantity, SomeUnit>(quantities, factory);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("factory");
            }

            [Fact]
            public void EmptySource_ShouldReturnDefaultTestQuantity()
            {
                // arrange
                var quantities = Enumerable.Empty<SomeQuantity>();
                Func<number, SomeUnit, SomeQuantity> factory = SomeQuantityFactory.Create;

                // act
                var result = EnumerableExtensions.Sum<SomeQuantity, SomeUnit>(quantities, factory);

                // assert
                result.Should().Be(default(SomeQuantity));
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var quantities = Fixture.CreateMany<SomeQuantity>(3);
                Func<number, SomeUnit, SomeQuantity> factory = SomeQuantityFactory.Create;

                number expectedResultInWatts = quantities.Sum(e => e.Value * e.Unit.ValueInUnits);
                var expectedResultUnit = quantities.First().Unit;
                var expectedResult = new SomeQuantity(expectedResultInWatts / expectedResultUnit.ValueInUnits, expectedResultUnit);

                // act
                var result = EnumerableExtensions.Sum<SomeQuantity, SomeUnit>(quantities, factory);

                // assert
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
