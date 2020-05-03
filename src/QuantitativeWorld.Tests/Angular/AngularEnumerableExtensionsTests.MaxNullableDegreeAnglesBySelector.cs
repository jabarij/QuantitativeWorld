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
        public class MaxNullableDegreeAnglesBySelector : AngularEnumerableExtensionsTests
        {
            public MaxNullableDegreeAnglesBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<DegreeAngle?>> source = null;
                Func<TestObject<DegreeAngle?>, DegreeAngle?> selector = e => e.Property;

                // act
                Action max = () => AngularEnumerableExtensions.Max(source, selector);

                // assert
                max.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void NullSelector_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<DegreeAngle?>>();
                Func<TestObject<DegreeAngle?>, DegreeAngle?> selector = null;

                // act
                Action max = () => AngularEnumerableExtensions.Max(source, selector);

                // assert
                max.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void EmptySource_ShouldReturnNull()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<DegreeAngle?>>();
                Func<TestObject<DegreeAngle?>, DegreeAngle?> selector = e => e.Property;

                // act
                var result = AngularEnumerableExtensions.Max(source, selector);

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<DegreeAngle?>(3).Select(e => new TestObject<DegreeAngle?>(e));
                Func<TestObject<DegreeAngle?>, DegreeAngle?> selector = e => e.Property;

                var expectedResult = source.OrderByDescending(e => e.Property.Value).First().Property;

                // act
                var result = AngularEnumerableExtensions.Max(source, selector);

                // assert
                result.Should().Be(expectedResult.Value);
            }
        }
    }
}
