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

    public partial class VolumeTests : TestsBase
    {
        public VolumeTests(TestFixture testFixture)
            : base(testFixture) { }

        private Volume CreateVolumeInUnit(VolumeUnit unit) =>
            Fixture
            .Create<Volume>()
            .Convert(unit);

        private Volume CreateVolumeInUnitOtherThan(params VolumeUnit[] unitsToExclude) =>
            Fixture
            .Create<Volume>()
            .Convert(CreateUnitOtherThan(unitsToExclude));

        private VolumeUnit CreateUnitOtherThan(params VolumeUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(VolumeUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
