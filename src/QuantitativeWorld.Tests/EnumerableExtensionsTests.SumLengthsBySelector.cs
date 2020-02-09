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
        public class SumLengthsBySelector : EnumerableExtensionsTests
        {
            public SumLengthsBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
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
            public void NullSelector_ShouldThrow()
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
            public void EmptySource_ShouldReturnDefaultLength()
            {
                // arrange
                var objects = Enumerable.Empty<TestObject<Length>>();

                // act
                var result = EnumerableExtensions.Sum(objects, e => e.Property);

                // assert
                result.Should().Be(default(Length));
            }

            [Fact]
            public void ShouldReturnValidResult()
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
