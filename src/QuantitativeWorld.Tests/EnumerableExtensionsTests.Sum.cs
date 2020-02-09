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
        public class Sum : EnumerableExtensionsTests
        {
            public Sum(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void Weights_NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Weight> weights = null;

                // act
                Action sum = () => EnumerableExtensions.Sum(weights);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void Weights_ShouldReturnValidResult()
            {
                // arrange
                var weights = Fixture.CreateMany<Weight>(3);
                decimal expectedResultInKilograms = weights.Sum(e => e.Kilograms);
                var expectedResultUnit = weights.First().Unit;
                var expectedResult = new Weight(expectedResultInKilograms).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Sum(weights);

                // assert
                result.Kilograms.Should().Be(expectedResult.Kilograms);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }

            [Fact]
            public void SelectedWeights_NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Weight>> objects = null;

                // act
                Action sum = () => EnumerableExtensions.Sum(objects, e => e.Property);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void SelectedWeights_NullSelector_ShouldThrow()
            {
                // arrange
                var objects = Enumerable.Empty<TestObject<Weight>>();

                // act
                Action sum = () => EnumerableExtensions.Sum(objects, null);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void SelectedWeights_ShouldReturnValidResult()
            {
                // arrange
                var objects = Fixture.CreateMany<Weight>(3).Select(e => new TestObject<Weight>(e));
                decimal expectedResultInKilograms = objects.Sum(e => e.Property.Kilograms);
                var expectedResultUnit = objects.First().Property.Unit;
                var expectedResult = new Weight(expectedResultInKilograms).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Sum(objects, e => e.Property);

                // assert
                result.Kilograms.Should().Be(expectedResult.Kilograms);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }

            class TestObject<T>
            {
                public TestObject(T property)
                {
                    Property = property;
                }

                public T Property { get; }
            }
        }
    }
}
