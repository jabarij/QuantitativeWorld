﻿using AutoFixture;
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
        public class MinNullableAngles : AngularEnumerableExtensionsTests
        {
            public MinNullableAngles(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Angle?> source = null;

                // act
                Action min = () => AngularEnumerableExtensions.Min(source);

                // assert
                min.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldReturnNull()
            {
                // arrange
                var source = Enumerable.Empty<Angle?>();

                // act
                var result = AngularEnumerableExtensions.Min(source);

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Angle?>(3);
                var expectedResult = source.OrderBy(e => e).First();

                // act
                var result = AngularEnumerableExtensions.Min(source);

                // assert
                result.Should().Be(expectedResult);
            }
        }
    }
}
