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
        public class SumLinearQuantitiesWithFactoryFunctionBySelector : EnumerableExtensionsTests
        {
            public SumLinearQuantitiesWithFactoryFunctionBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<SomeQuantity>> objects = null;
                Func<double, SomeUnit, SomeQuantity> factory = SomeQuantityFactory.Create;
                Func<TestObject<SomeQuantity>, SomeQuantity> selector = e => e.Property;

                // act
                Action sum = () => EnumerableExtensions.Sum<TestObject<SomeQuantity>, SomeQuantity, SomeUnit>(objects, factory, selector);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void NullSelector_ShouldThrow()
            {
                // arrange
                var objects = Enumerable.Empty<TestObject<SomeQuantity>>();
                Func<double, SomeUnit, SomeQuantity> factory = SomeQuantityFactory.Create;
                Func<TestObject<SomeQuantity>, SomeQuantity> selector = null;

                // act
                Action sum = () => EnumerableExtensions.Sum<TestObject<SomeQuantity>, SomeQuantity, SomeUnit>(objects, factory, selector);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void NullFactory_ShouldThrow()
            {
                // arrange
                var objects = Enumerable.Empty<TestObject<SomeQuantity>>();
                Func<double, SomeUnit, SomeQuantity> factory = null;
                Func<TestObject<SomeQuantity>, SomeQuantity> selector = e => e.Property;

                // act
                Action sum = () => EnumerableExtensions.Sum<TestObject<SomeQuantity>, SomeQuantity, SomeUnit>(objects, factory, selector);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("factory");
            }

            [Fact]
            public void EmptySource_ShouldReturnDefaultTestQuantity()
            {
                // arrange
                var objects = Enumerable.Empty<TestObject<SomeQuantity>>();
                Func<double, SomeUnit, SomeQuantity> factory = SomeQuantityFactory.Create;
                Func<TestObject<SomeQuantity>, SomeQuantity> selector = e => e.Property;

                // act
                var result = EnumerableExtensions.Sum<TestObject<SomeQuantity>, SomeQuantity, SomeUnit>(objects, factory, selector);

                // assert
                result.Should().Be(default(SomeQuantity));
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var objects = Fixture.CreateMany<SomeQuantity>(3).Select(e => new TestObject<SomeQuantity>(e));
                Func<double, SomeUnit, SomeQuantity> factory = SomeQuantityFactory.Create;
                Func<TestObject<SomeQuantity>, SomeQuantity> selector = e => e.Property;

                double expectedResultInWatts = objects.Sum(e => e.Property.Value * e.Property.Unit.ValueInUnits);
                var expectedResultUnit = objects.First().Property.Unit;
                var expectedResult = new SomeQuantity(expectedResultInWatts / expectedResultUnit.ValueInUnits, expectedResultUnit);

                // act
                var result = EnumerableExtensions.Sum<TestObject<SomeQuantity>, SomeQuantity, SomeUnit>(objects, factory, selector);

                // assert
                result.Value.Should().Be(expectedResult.Value);
                result.Unit.Should().Be(expectedResult.Unit);
            }
        }
    }
}
