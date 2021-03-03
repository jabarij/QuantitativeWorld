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

    public partial class VolumeUnitTests : TestsBase
    {
        public VolumeUnitTests(TestFixture testFixture)
            : base(testFixture) { }

        private VolumeUnit CreateUnitOtherThan(params VolumeUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(VolumeUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
