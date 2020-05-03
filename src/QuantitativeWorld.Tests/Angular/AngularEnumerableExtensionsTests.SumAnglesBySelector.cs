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
        public class SumAnglesBySelector : AngularEnumerableExtensionsTests
        {
            public SumAnglesBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Angle>> objects = null;
                Func<TestObject<Angle>, Angle> selector = e => e.Property;

                // act
                Action sum = () => AngularEnumerableExtensions.Sum(objects, selector);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void NullSelector_ShouldThrow()
            {
                // arrange
                var objects = Enumerable.Empty<TestObject<Angle>>();
                Func<TestObject<Angle>, Angle> selector = null;

                // act
                Action sum = () => AngularEnumerableExtensions.Sum(objects, selector);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void EmptySource_ShouldReturnDefaultAngle()
            {
                // arrange
                var objects = Enumerable.Empty<TestObject<Angle>>();

                // act
                var result = AngularEnumerableExtensions.Sum(objects, e => e.Property);

                // assert
                result.Should().Be(default(Angle));
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var objects = Fixture.CreateMany<Angle>(3).Select(e => new TestObject<Angle>(e));
                double expectedResultInTurns = objects.Sum(e => e.Property.Turns);
                var expectedResultUnit = objects.First().Property.Unit;
                var expectedResult = new Angle(expectedResultInTurns).Convert(expectedResultUnit);

                // act
                var result = AngularEnumerableExtensions.Sum(objects, e => e.Property);

                // assert
                result.Turns.Should().Be(expectedResult.Turns);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}
