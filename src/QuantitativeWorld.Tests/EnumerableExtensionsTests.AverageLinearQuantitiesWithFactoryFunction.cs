using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
    using Constants = DoubleConstants;
    using number = System.Double;
#endif

    partial class EnumerableExtensionsTests
    {
        public class AverageLinearQuantitiesWithFactoryFunction : EnumerableExtensionsTests
        {
            public AverageLinearQuantitiesWithFactoryFunction(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<SomeQuantity> source = null;
                Func<number, SomeUnit, SomeQuantity> factory = SomeQuantityFactory.Create;

                // act
                Action average = () => EnumerableExtensions.Average<SomeQuantity, SomeUnit>(source, factory);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void NullFactory_ShouldThrow()
            {
                // arrange
                var source = Fixture.CreateMany<SomeQuantity>(3);
                Func<number, SomeUnit, SomeQuantity> factory = null;

                // act
                Action average = () => EnumerableExtensions.Average<SomeQuantity, SomeUnit>(source, factory);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("factory");
            }

            [Fact]
            public void EmptySource_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<SomeQuantity>();
                Func<number, SomeUnit, SomeQuantity> factory = SomeQuantityFactory.Create;

                // act
                Action average = () => EnumerableExtensions.Average(source, factory);

                // assert
                average.Should().Throw<InvalidOperationException>();
            }

            [Fact]

            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<SomeQuantity>(3);
                Func<number, SomeUnit, SomeQuantity> factory = SomeQuantityFactory.Create;

                number expectedResultInWatts = source.Average(e => e.Value * e.Unit.ValueInUnits);
                var expectedResultUnit = source.First().Unit;
                var expectedResult = new SomeQuantity(expectedResultInWatts / expectedResultUnit.ValueInUnits, expectedResultUnit);

                // act
                var result = EnumerableExtensions.Average<SomeQuantity, SomeUnit>(source, factory);

                // assert
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().BeApproximately(expectedResult.Value);
            }
        }
    }
}
