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
        public class SumLengths : EnumerableExtensionsTests
        {
            public SumLengths(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void Lengths_NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Length> lengths = null;

                // act
                Action sum = () => EnumerableExtensions.Sum(lengths);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void Lengths_EmptySource_ShouldReturnDefaultLength()
            {
                // arrange
                var lengths = Enumerable.Empty<Length>();

                // act
                var result = EnumerableExtensions.Sum(lengths);

                // assert
                result.Should().Be(default(Length));
            }

            [Fact]
            public void Lengths_ShouldReturnValidResult()
            {
                // arrange
                var lengths = Fixture.CreateMany<Length>(3);
                decimal expectedResultInMetres = lengths.Sum(e => e.Metres);
                var expectedResultUnit = lengths.First().Unit;
                var expectedResult = new Length(expectedResultInMetres).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Sum(lengths);

                // assert
                result.Metres.Should().Be(expectedResult.Metres);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }

            [Fact]
            public void SelectedLengths_NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Length>> objects = null;
                Func<TestObject<Length>, Length> selector = e => e.Property;

                // act
                Action sum = () => EnumerableExtensions.Sum(objects, selector);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void SelectedLengths_NullSelector_ShouldThrow()
            {
                // arrange
                var objects = Enumerable.Empty<TestObject<Length>>();
                Func<TestObject<Length>, Length> selector = null;

                // act
                Action sum = () => EnumerableExtensions.Sum(objects, selector);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void SelectedLengths_EmptySource_ShouldReturnDefaultLength()
            {
                // arrange
                var objects = Enumerable.Empty<TestObject<Length>>();

                // act
                var result = EnumerableExtensions.Sum(objects, e => e.Property);

                // assert
                result.Should().Be(default(Length));
            }

            [Fact]
            public void SelectedLengths_ShouldReturnValidResult()
            {
                // arrange
                var objects = Fixture.CreateMany<Length>(3).Select(e => new TestObject<Length>(e));
                decimal expectedResultInMetres = objects.Sum(e => e.Property.Metres);
                var expectedResultUnit = objects.First().Property.Unit;
                var expectedResult = new Length(expectedResultInMetres).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Sum(objects, e => e.Property);

                // assert
                result.Metres.Should().Be(expectedResult.Metres);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
