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

    public partial class EnergyTests : TestsBase
    {
        public EnergyTests(TestFixture testFixture)
            : base(testFixture) { }

        private Energy CreateEnergyInUnit(EnergyUnit unit) =>
            Fixture
            .Create<Energy>()
            .Convert(unit);

        private Energy CreateEnergyInUnitOtherThan(params EnergyUnit[] unitsToExclude) =>
            Fixture
            .Create<Energy>()
            .Convert(CreateUnitOtherThan(unitsToExclude));

        private EnergyUnit CreateUnitOtherThan(params EnergyUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(EnergyUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
