using AutoFixture;
using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
#endif
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
