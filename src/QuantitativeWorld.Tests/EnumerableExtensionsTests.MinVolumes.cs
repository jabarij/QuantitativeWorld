﻿using AutoFixture;
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
        public class MinVolumes : EnumerableExtensionsTests
        {
            public MinVolumes(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Volume> source = null;

                // act
                Action min = () => EnumerableExtensions.Min(source);

                // assert
                min.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<Volume>();

                // act
                Action min = () => EnumerableExtensions.Min(source);

                // assert
                min.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Volume>(3);

                var expectedResult = source.OrderBy(e => e).First();

                // act
                var result = EnumerableExtensions.Min(source);

                // assert
                result.Should().Be(expectedResult);
            }
        }
    }
}