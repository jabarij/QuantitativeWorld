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
                IEnumerable<TestObject<Power>> objects = null;
                Func<decimal, PowerUnit, Power> factory = PowerFactory.Create;
                Func<TestObject<Power>, Power> selector = e => e.Property;

                // act
                Action sum = () => EnumerableExtensions.Sum<TestObject<Power>, Power, PowerUnit>(objects, factory, selector);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void NullSelector_ShouldThrow()
            {
                // arrange
                var objects = Enumerable.Empty<TestObject<Power>>();
                Func<decimal, PowerUnit, Power> factory = PowerFactory.Create;
                Func<TestObject<Power>, Power> selector = null;

                // act
                Action sum = () => EnumerableExtensions.Sum<TestObject<Power>, Power, PowerUnit>(objects, factory, selector);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void NullFactory_ShouldThrow()
            {
                // arrange
                var objects = Enumerable.Empty<TestObject<Power>>();
                Func<decimal, PowerUnit, Power> factory = null;
                Func<TestObject<Power>, Power> selector = e => e.Property;

                // act
                Action sum = () => EnumerableExtensions.Sum<TestObject<Power>, Power, PowerUnit>(objects, factory, selector);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("factory");
            }

            [Fact]
            public void EmptySource_ShouldReturnDefaultTestQuantity()
            {
                // arrange
                var objects = Enumerable.Empty<TestObject<Power>>();
                Func<decimal, PowerUnit, Power> factory = PowerFactory.Create;
                Func<TestObject<Power>, Power> selector = e => e.Property;

                // act
                var result = EnumerableExtensions.Sum<TestObject<Power>, Power, PowerUnit>(objects, factory, selector);

                // assert
                result.Should().Be(default(Power));
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var objects = Fixture.CreateMany<Power>(3).Select(e => new TestObject<Power>(e));
                Func<decimal, PowerUnit, Power> factory = PowerFactory.Create;
                Func<TestObject<Power>, Power> selector = e => e.Property;

                decimal expectedResultInWatts = objects.Sum(e => e.Property.Value * e.Property.Unit.ValueInWatts);
                var expectedResultUnit = objects.First().Property.Unit;
                var expectedResult = new Power(expectedResultInWatts / expectedResultUnit.ValueInWatts, expectedResultUnit);

                // act
                var result = EnumerableExtensions.Sum<TestObject<Power>, Power, PowerUnit>(objects, factory, selector);

                // assert
                result.Value.Should().Be(expectedResult.Value);
                result.Unit.Should().Be(expectedResult.Unit);
            }
        }
    }
}
