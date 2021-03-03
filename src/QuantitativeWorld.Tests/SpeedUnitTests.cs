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

    public partial class SpeedUnitTests : TestsBase
    {
        public SpeedUnitTests(TestFixture testFixture)
            : base(testFixture) { }

        private SpeedUnit CreateUnitOtherThan(params SpeedUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(SpeedUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
