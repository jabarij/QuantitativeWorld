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

    public partial class EnergyUnitTests : TestsBase
    {
        public EnergyUnitTests(TestFixture testFixture)
            : base(testFixture) { }

        private EnergyUnit CreateUnitOtherThan(params EnergyUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(EnergyUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
