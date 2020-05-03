using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
    partial class AngularEnumerableExtensionsTests
    {
        public class AverageAnglesBySelector : AngularEnumerableExtensionsTests
        {
            public AverageAnglesBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Angle>> source = null;
                Func<TestObject<Angle>, Angle> selector = e => e.Property;

                // act
                Action average = () => AngularEnumerableExtensions.Average(source, selector);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void NullSelector_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<Angle>>();
                Func<TestObject<Angle>, Angle> selector = null;

                // act
                Action average = () => AngularEnumerableExtensions.Average(source, selector);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void EmptySource_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<Angle>>();
                Func<TestObject<Angle>, Angle> selector = e => e.Property;

                // act
                Action average = () => AngularEnumerableExtensions.Average(source, selector);

                // assert
                average.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Angle>(3).Select(e => new TestObject<Angle>(e));
                Func<TestObject<Angle>, Angle> selector = e => e.Property;

                double expectedResultInTurns = source.Average(e => e.Property.Turns);
                var expectedResultUnit = source.First().Property.Unit;
                var expectedResult = new Angle(expectedResultInTurns).Convert(expectedResultUnit);

                // act
                var result = AngularEnumerableExtensions.Average(source, selector);

                // assert
                result.Turns.Should().Be(expectedResult.Turns);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
