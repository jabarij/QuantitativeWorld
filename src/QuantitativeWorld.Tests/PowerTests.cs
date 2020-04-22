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
            Fixture
            .Create<Power>()
            .Convert(unit);

        private Power CreatePowerInUnitOtherThan(params PowerUnit[] unitsToExclude) =>
            Fixture
            .Create<Power>()
            .Convert(CreateUnitOtherThan(unitsToExclude));

        private PowerUnit CreateUnitOtherThan(params PowerUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(PowerUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
