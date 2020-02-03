using AutoFixture;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class WeightTests
    {
        public class Addition : WeightTests
        {
            public Addition(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void WeightsOfSameUnit_ShouldProduceValidResultInKilograms()
            {
                // arrange
                var weight1 = Fixture.Create<Weight>();
                var weight2 = new Weight(Fixture.Create<decimal>(), weight1.Unit);

                // act
                var result = weight1 + weight2;

                // assert
                result.Kilograms.Should().Be(weight1.Kilograms + weight2.Kilograms);
            }

            [Fact]
            public void WeightOfDifferentUnits_ShouldProduceValidResultInKilograms()
            {
                // arrange
                var weight1 = Fixture.Create<Weight>();
                var weight2 = new Weight(
                    value: Fixture.Create<decimal>(),
                    unit: Fixture.CreateFromSet(WeightUnit.GetKnownUnits().Except(new[] { weight1.Unit })));
                weight1.Unit.Should().NotBe(weight2.Unit, because: "test assumes that two weight of different units are added");

                // act
                var result = weight1 + weight2;

                // assert
                result.Kilograms.Should().Be(weight1.Kilograms + weight2.Kilograms);
            }

            [Fact]
            public void WeightOfDifferentUnits_ShouldProduceResultOfSameUnitAsLeftOperand()
            {
                // arrange
                var weight1 = Fixture.Create<Weight>();
                var weight2 = new Weight(
                    value: Fixture.Create<decimal>(),
                    unit: Fixture.CreateFromSet(WeightUnit.GetKnownUnits().Except(new[] { weight1.Unit })));
                weight1.Unit.Should().NotBe(weight2.Unit, because: "test assumes that two weight of different units are added");

                // act
                var result = weight1 + weight2;

                // assert
                result.Unit.Should().Be(weight1.Unit);
            }
        }

        public class Subtraction : WeightTests
        {
            public Subtraction(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void WeightsOfSameUnit_ShouldProduceValidResultInKilograms()
            {
                // arrange
                var weight1 = Fixture.Create<Weight>();
                var weight2 = new Weight(Fixture.Create<decimal>(), weight1.Unit);

                // act
                var result = weight1 - weight2;

                // assert
                result.Kilograms.Should().Be(weight1.Kilograms - weight2.Kilograms);
            }

            [Fact]
            public void WeightOfDifferentUnits_ShouldProduceValidResultInKilograms()
            {
                // arrange
                var weight1 = Fixture.Create<Weight>();
                var weight2 = new Weight(
                    value: Fixture.Create<decimal>(),
                    unit: Fixture.CreateFromSet(WeightUnit.GetKnownUnits().Except(new[] { weight1.Unit })));
                weight1.Unit.Should().NotBe(weight2.Unit, because: "test assumes that two weight of different units are subtracted");

                // act
                var result = weight1 - weight2;

                // assert
                result.Kilograms.Should().Be(weight1.Kilograms - weight2.Kilograms);
            }

            [Fact]
            public void WeightOfDifferentUnits_ShouldProduceResultOfSameUnitAsLeftOperand()
            {
                // arrange
                var weight1 = Fixture.Create<Weight>();
                var weight2 = new Weight(
                    value: Fixture.Create<decimal>(),
                    unit: Fixture.CreateFromSet(WeightUnit.GetKnownUnits().Except(new[] { weight1.Unit })));
                weight1.Unit.Should().NotBe(weight2.Unit, because: "test assumes that two weight of different units are subtracted");

                // act
                var result = weight1 - weight2;

                // assert
                result.Unit.Should().Be(weight1.Unit);
            }
        }

        public class Multiplication : WeightTests
        {
            public Multiplication(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void MultiplyByDecimal_ShouldProduceValidResultInKilograms()
            {
                // arrange
                var weight = Fixture.Create<Weight>();
                decimal factor = Fixture.Create<decimal>();

                // act
                var result = weight * factor;

                // assert
                result.Kilograms.Should().Be(weight.Kilograms * factor);
            }

            [Fact]
            public void MultiplyByDecimal_ShouldProduceResultWithSameUnit()
            {
                // arrange
                var weight = Fixture.Create<Weight>();
                decimal factor = Fixture.Create<decimal>();

                // act
                var result = weight * factor;

                // assert
                result.Unit.Should().Be(weight.Unit);
            }
        }

        public class Division : WeightTests
        {
            public Division(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DivideByDecimal_ShouldProduceValidResultInKilograms()
            {
                // arrange
                var weight = Fixture.Create<Weight>();
                decimal denominator = Fixture.CreateNonZero();

                // act
                var result = weight / denominator;

                // assert
                result.Kilograms.Should().Be(weight.Kilograms / denominator);
            }

            [Fact]
            public void DivideByDecimal_ShouldProduceResultWithSameUnit()
            {
                // arrange
                var weight = Fixture.Create<Weight>();
                decimal denominator = Fixture.CreateNonZero();

                // act
                var result = weight * denominator;

                // assert
                result.Unit.Should().Be(weight.Unit);
            }
        }
    }
}
