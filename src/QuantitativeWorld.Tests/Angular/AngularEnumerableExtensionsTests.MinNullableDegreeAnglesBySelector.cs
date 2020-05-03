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
        public class MinNullableDegreeAnglesBySelector : AngularEnumerableExtensionsTests
        {
            public MinNullableDegreeAnglesBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<DegreeAngle?>> source = null;
                Func<TestObject<DegreeAngle?>, DegreeAngle?> selector = e => e.Property;

                // act
                Action min = () => AngularEnumerableExtensions.Min(source, selector);

                // assert
                min.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void NullSelector_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<DegreeAngle?>>();
                Func<TestObject<DegreeAngle?>, DegreeAngle?> selector = null;

                // act
                Action min = () => AngularEnumerableExtensions.Min(source, selector);

                // assert
                min.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void EmptySource_ShouldReturnNull()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<DegreeAngle?>>();
                Func<TestObject<DegreeAngle?>, DegreeAngle?> selector = e => e.Property;

                // act
                var result = AngularEnumerableExtensions.Min(source, selector);

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<DegreeAngle?>(3).Select(e => new TestObject<DegreeAngle?>(e));
                Func<TestObject<DegreeAngle?>, DegreeAngle?> selector = e => e.Property;

                var expectedResult = source.OrderBy(e => e.Property.Value).First().Property;

                // act
                var result = AngularEnumerableExtensions.Min(source, selector);

                // assert
                result.Should().Be(expectedResult.Value);
            }
        }
    }
}
