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
        public class AveragePowersBySelector : EnumerableExtensionsTests
        {
            public AveragePowersBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Power>> source = null;
                Func<TestObject<Power>, Power> selector = e => e.Property;

                // act
                Action average = () => EnumerableExtensions.Average(source, selector);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void NullSelector_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<Power>>();
                Func<TestObject<Power>, Power> selector = null;

                // act
                Action average = () => EnumerableExtensions.Average(source, selector);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void EmptySource_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<Power>>();
                Func<TestObject<Power>, Power> selector = e => e.Property;

                // act
                Action average = () => EnumerableExtensions.Average(source, selector);

                // assert
                average.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Power>(3).Select(e => new TestObject<Power>(e));
                Func<TestObject<Power>, Power> selector = e => e.Property;

                double expectedResultInWatts = source.Average(e => e.Property.Watts);
                var expectedResultUnit = source.First().Property.Unit;
                var expectedResult = new Power(expectedResultInWatts).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Average(source, selector);

                // assert
                result.Watts.Should().Be(expectedResult.Watts);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
