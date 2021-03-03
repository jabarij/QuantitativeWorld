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
        public class AverageLinearQuantitiesWithFactoryFunctionBySelector : EnumerableExtensionsTests
        {
            public AverageLinearQuantitiesWithFactoryFunctionBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<SomeQuantity>> source = null;
                Func<number, SomeUnit, SomeQuantity> factory = SomeQuantityFactory.Create;
                Func<TestObject<SomeQuantity>, SomeQuantity> selector = e => e.Property;

                // act
                Action average = () => EnumerableExtensions.Average<TestObject<SomeQuantity>, SomeQuantity, SomeUnit>(source, factory, selector);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void NullSelector_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<SomeQuantity>>();
                Func<number, SomeUnit, SomeQuantity> factory = SomeQuantityFactory.Create;
                Func<TestObject<SomeQuantity>, SomeQuantity> selector = null;

                // act
                Action average = () => EnumerableExtensions.Average<TestObject<SomeQuantity>, SomeQuantity, SomeUnit>(source, factory, selector);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void NullFactory_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<SomeQuantity>>();
                Func<number, SomeUnit, SomeQuantity> factory = null;
                Func<TestObject<SomeQuantity>, SomeQuantity> selector = e => e.Property;

                // act
                Action average = () => EnumerableExtensions.Average<TestObject<SomeQuantity>, SomeQuantity, SomeUnit>(source, factory, selector);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("factory");
            }

            [Fact]
            public void EmptySource_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<SomeQuantity>>();
                Func<number, SomeUnit, SomeQuantity> factory = SomeQuantityFactory.Create;
                Func<TestObject<SomeQuantity>, SomeQuantity> selector = e => e.Property;

                // act
                Action average = () => EnumerableExtensions.Average<TestObject<SomeQuantity>, SomeQuantity, SomeUnit>(source, factory, selector);

                // assert
                average.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<SomeQuantity>(3).Select(e => new TestObject<SomeQuantity>(e));
                Func<number, SomeUnit, SomeQuantity> factory = SomeQuantityFactory.Create;
                Func<TestObject<SomeQuantity>, SomeQuantity> selector = e => e.Property;

                number expectedResultInWatts = source.Average(e => e.Property.Value * e.Property.Unit.ValueInUnits);
                var expectedResultUnit = source.First().Property.Unit;
                var expectedResult = new SomeQuantity(expectedResultInWatts / expectedResultUnit.ValueInUnits, expectedResultUnit);

                // act
                var result = EnumerableExtensions.Average<TestObject<SomeQuantity>, SomeQuantity, SomeUnit>(source, factory, selector);

                // assert
                result.Value.Should().BeApproximately(expectedResult.Value);
                result.Unit.Should().Be(expectedResult.Unit);
            }
        }
    }
}
