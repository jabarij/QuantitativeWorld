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
        public class AverageWeightsBySelector : EnumerableExtensionsTests
        {
            public AverageWeightsBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Weight>> source = null;
                Func<TestObject<Weight>, Weight> selector = e => e.Property;

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
                var source = Enumerable.Empty<TestObject<Weight>>();
                Func<TestObject<Weight>, Weight> selector = null;

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
                var source = Enumerable.Empty<TestObject<Weight>>();
                Func<TestObject<Weight>, Weight> selector = e => e.Property;

                // act
                Action average = () => EnumerableExtensions.Average(source, selector);

                // assert
                average.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Weight>(3).Select(e => new TestObject<Weight>(e));
                Func<TestObject<Weight>, Weight> selector = e => e.Property;

                double expectedResultInKilograms = source.Average(e => e.Property.Kilograms);
                var expectedResultUnit = source.First().Property.Unit;
                var expectedResult = new Weight(expectedResultInKilograms).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Average(source, selector);

                // assert
                result.Kilograms.Should().Be(expectedResult.Kilograms);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
