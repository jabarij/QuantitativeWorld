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
        public class AverageLinearQuantitiesWithFactoryFunctionBySelector : EnumerableExtensionsTests
        {
            public AverageLinearQuantitiesWithFactoryFunctionBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Power>> source = null;
                Func<double, PowerUnit, Power> factory = PowerFactory.Create;
                Func<TestObject<Power>, Power> selector = e => e.Property;

                // act
                Action average = () => EnumerableExtensions.Average<TestObject<Power>, Power, PowerUnit>(source, factory, selector);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void NullSelector_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<Power>>();
                Func<double, PowerUnit, Power> factory = PowerFactory.Create;
                Func<TestObject<Power>, Power> selector = null;

                // act
                Action average = () => EnumerableExtensions.Average<TestObject<Power>, Power, PowerUnit>(source, factory, selector);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void NullFactory_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<Power>>();
                Func<double, PowerUnit, Power> factory = null;
                Func<TestObject<Power>, Power> selector = e => e.Property;

                // act
                Action average = () => EnumerableExtensions.Average<TestObject<Power>, Power, PowerUnit>(source, factory, selector);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("factory");
            }

            [Fact]
            public void EmptySource_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<Power>>();
                Func<double, PowerUnit, Power> factory = PowerFactory.Create;
                Func<TestObject<Power>, Power> selector = e => e.Property;

                // act
                Action average = () => EnumerableExtensions.Average<TestObject<Power>, Power, PowerUnit>(source, factory, selector);

                // assert
                average.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Power>(3).Select(e => new TestObject<Power>(e));
                Func<double, PowerUnit, Power> factory = PowerFactory.Create;
                Func<TestObject<Power>, Power> selector = e => e.Property;

                double expectedResultInWatts = source.Average(e => e.Property.Value * e.Property.Unit.ValueInWatts);
                var expectedResultUnit = source.First().Property.Unit;
                var expectedResult = new Power(expectedResultInWatts / expectedResultUnit.ValueInWatts, expectedResultUnit);

                // act
                var result = EnumerableExtensions.Average<TestObject<Power>, Power, PowerUnit>(source, factory, selector);

                // assert
                result.Value.Should().BeApproximately(expectedResult.Value, DoublePrecision);
                result.Unit.Should().Be(expectedResult.Unit);
            }
        }
    }
}
