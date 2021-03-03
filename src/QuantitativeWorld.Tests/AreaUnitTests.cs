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

    public partial class AreaUnitTests : TestsBase
    {
        public AreaUnitTests(TestFixture testFixture)
            : base(testFixture) { }

        private AreaUnit CreateUnitOtherThan(params AreaUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(AreaUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
