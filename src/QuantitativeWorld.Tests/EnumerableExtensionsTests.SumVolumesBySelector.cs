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
        public class SumVolumesBySelector : EnumerableExtensionsTests
        {
            public SumVolumesBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Volume>> objects = null;
                Func<TestObject<Volume>, Volume> selector = e => e.Property;

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
                var objects = Enumerable.Empty<TestObject<Volume>>();
                Func<TestObject<Volume>, Volume> selector = null;

                // act
                Action sum = () => EnumerableExtensions.Sum(objects, selector);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void EmptySource_ShouldReturnDefaultVolume()
            {
                // arrange
                var objects = Enumerable.Empty<TestObject<Volume>>();

                // act
                var result = EnumerableExtensions.Sum(objects, e => e.Property);

                // assert
                result.Should().Be(default(Volume));
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var objects = Fixture.CreateMany<Volume>(3).Select(e => new TestObject<Volume>(e));
                double expectedResultInMetres = objects.Sum(e => e.Property.CubicMetres);
                var expectedResultUnit = objects.First().Property.Unit;
                var expectedResult = new Volume(expectedResultInMetres).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Sum(objects, e => e.Property);

                // assert
                result.CubicMetres.Should().Be(expectedResult.CubicMetres);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
