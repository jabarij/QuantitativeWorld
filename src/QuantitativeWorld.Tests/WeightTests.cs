using AutoFixture;
using QuantitativeWorld.TestAbstractions;
using System.Linq;

namespace QuantitativeWorld.Tests
{
    public partial class WeightTests : TestsBase
    {
        public WeightTests(TestFixture testFixture)
            : base(testFixture) { }

        private Weight CreateWeightInUnit(WeightUnit unit) =>
            new Weight(
                value: Fixture.Create<decimal>(),
                unit: unit);

        private Weight CreateWeightInUnitOtherThan(params WeightUnit[] unitsToExclude) =>
            new Weight(
                value: Fixture.Create<decimal>(),
                unit: CreateUnitOtherThan(unitsToExclude));

        private WeightUnit CreateUnitOtherThan(params WeightUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(WeightUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
