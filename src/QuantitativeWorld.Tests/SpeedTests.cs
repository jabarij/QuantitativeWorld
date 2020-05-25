using AutoFixture;
using QuantitativeWorld.TestAbstractions;
using System.Linq;

namespace QuantitativeWorld.Tests
{
    public partial class SpeedTests : TestsBase
    {
        public SpeedTests(TestFixture testFixture)
            : base(testFixture) { }

        private Speed CreateSpeedInUnit(SpeedUnit unit) =>
            Fixture
            .Create<Speed>()
            .Convert(unit);

        private Speed CreateSpeedInUnitOtherThan(params SpeedUnit[] unitsToExclude) =>
            Fixture
            .Create<Speed>()
            .Convert(CreateUnitOtherThan(unitsToExclude));

        private SpeedUnit CreateUnitOtherThan(params SpeedUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(SpeedUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
