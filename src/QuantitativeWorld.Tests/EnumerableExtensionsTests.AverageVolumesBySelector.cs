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
        public class AverageVolumesBySelector : EnumerableExtensionsTests
        {
            public AverageVolumesBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Volume>> source = null;
                Func<TestObject<Volume>, Volume> selector = e => e.Property;

                // act
                Action average = () => EnumerableExtensions.Average(source, selector);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void NullSelector_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<Volume>>();
                Func<TestObject<Volume>, Volume> selector = null;

                // act
                Action average = () => EnumerableExtensions.Average(source, selector);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void EmptySource_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<Volume>>();
                Func<TestObject<Volume>, Volume> selector = e => e.Property;

                // act
                Action average = () => EnumerableExtensions.Average(source, selector);

                // assert
                average.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Volume>(3).Select(e => new TestObject<Volume>(e));
                Func<TestObject<Volume>, Volume> selector = e => e.Property;

                double expectedResultInCubicMetre = source.Average(e => e.Property.CubicMetres);
                var expectedResultUnit = source.First().Property.Unit;
                var expectedResult = new Volume(expectedResultInCubicMetre).Convert(expectedResultUnit);

                // act
                var result = EnumerableExtensions.Average(source, selector);

                // assert
                result.CubicMetres.Should().Be(expectedResult.CubicMetres);
                result.Unit.Should().Be(expectedResult.Unit);
                result.Value.Should().Be(expectedResult.Value);
            }
        }
    }
}