using AutoFixture;
using QuantitativeWorld.TestAbstractions;
using System.Linq;

namespace QuantitativeWorld.Tests
{
    public partial class PowerTests : TestsBase
    {
        public PowerTests(TestFixture testFixture)
            : base(testFixture) { }

        private Power CreatePowerInUnit(PowerUnit unit) =>
            new Power(
                value: Fixture.Create<decimal>(),
                unit: unit);

        private Power CreatePowerInUnitOtherThan(params PowerUnit[] unitsToExclude) =>
            new Power(
                value: Fixture.Create<decimal>(),
                unit: CreateUnitOtherThan(unitsToExclude));

        private PowerUnit CreateUnitOtherThan(params PowerUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(PowerUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
