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
        public class SumDegreeAnglesBySelector : AngularEnumerableExtensionsTests
        {
            public SumDegreeAnglesBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<DegreeAngle>> objects = null;
                Func<TestObject<DegreeAngle>, DegreeAngle> selector = e => e.Property;

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
                var objects = Enumerable.Empty<TestObject<DegreeAngle>>();
                Func<TestObject<DegreeAngle>, DegreeAngle> selector = null;

                // act
                Action sum = () => AngularEnumerableExtensions.Sum(objects, selector);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void EmptySource_ShouldReturnDefaultDegreeAngle()
            {
                // arrange
                var objects = Enumerable.Empty<TestObject<DegreeAngle>>();

                // act
                var result = AngularEnumerableExtensions.Sum(objects, e => e.Property);

                // assert
                result.Should().Be(default(DegreeAngle));
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var objects = Fixture.CreateMany<DegreeAngle>(3).Select(e => new TestObject<DegreeAngle>(e));
                double expectedResultInSeconds = objects.Sum(e => e.Property.TotalSeconds);
                var expectedResult = new DegreeAngle(expectedResultInSeconds);

                // act
                var result = AngularEnumerableExtensions.Sum(objects, e => e.Property);

                // assert
                result.TotalSeconds.Should().Be(expectedResult.TotalSeconds);
            }
        }
    }
}
