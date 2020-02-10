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
                IEnumerable<TestObject<Weight>> objects = null;
                Func<TestObject<Weight>, Weight> selector = e => e.Property;

                // act
                Action average = () => EnumerableExtensions.Average(objects, selector);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void NullSelector_ShouldThrow()
            {
                // arrange
                var objects = Enumerable.Empty<TestObject<Weight>>();
                Func<TestObject<Weight>, Weight> selector = null;

                // act
                Action average = () => EnumerableExtensions.Average(objects, selector);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void EmptySource_ShouldReturnDefaultWeight()
            {
                // arrange
                var objects = Enumerable.Empty<TestObject<Weight>>();

                // act
                var result = EnumerableExtensions.Average(objects, e => e.Property);

                // assert
                result.Should().Be(default(Weight));
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var objects = Fixture.CreateMany<Weight>(3).Select(e => new TestObject<Weight>(e));
                decimal expectedResultInKilograms = objects.Average(e => e.Property.Kilograms);
                var expectedResultUnit = objects.First().Property.Unit;
                var expectedResult = new Weight(expectedResultInKilograms).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Average(objects, e => e.Property);

                // assert
                result.Kilograms.Should().Be(expectedResult.Kilograms);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
