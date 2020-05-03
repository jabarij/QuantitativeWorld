using AutoFixture;
using QuantitativeWorld.TestAbstractions;
using System.Linq;

namespace QuantitativeWorld.Tests
{
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
