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
        public class AverageAreasBySelector : EnumerableExtensionsTests
        {
            public AverageAreasBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Area>> source = null;
                Func<TestObject<Area>, Area> selector = e => e.Property;

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
                var source = Enumerable.Empty<TestObject<Area>>();
                Func<TestObject<Area>, Area> selector = null;

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
                var source = Enumerable.Empty<TestObject<Area>>();
                Func<TestObject<Area>, Area> selector = e => e.Property;

                // act
                Action average = () => EnumerableExtensions.Average(source, selector);

                // assert
                average.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Area>(3).Select(e => new TestObject<Area>(e));
                Func<TestObject<Area>, Area> selector = e => e.Property;

                double expectedResultInSquareMetres = source.Average(e => e.Property.SquareMetres);
                var expectedResultUnit = source.First().Property.Unit;
                var expectedResult = new Area(expectedResultInSquareMetres).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Average(source, selector);

                // assert
                result.SquareMetres.Should().Be(expectedResult.SquareMetres);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
