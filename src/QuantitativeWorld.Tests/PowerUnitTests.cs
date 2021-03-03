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

    public partial class PowerUnitTests : TestsBase
    {
        public PowerUnitTests(TestFixture testFixture)
            : base(testFixture) { }

        private PowerUnit CreateUnitOtherThan(params PowerUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(PowerUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
