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
        public class AveragePowers : EnumerableExtensionsTests
        {
            public AveragePowers(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Power> source = null;

                // act
                Action average = () => EnumerableExtensions.Average(source);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<Power>();

                // act
                Action average = () => EnumerableExtensions.Average(source);

                // assert
                average.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Power>(3);

                double expectedResultInWatts = source.Average(e => e.Watts);
                var expectedResultUnit = source.First().Unit;
                var expectedResult = new Power(expectedResultInWatts).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Average(source);

                // assert
                result.Watts.Should().Be(expectedResult.Watts);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}