using QuantitativeWorld.TestAbstractions;
using System.Linq;

namespace QuantitativeWorld.Tests
{
    public partial class VolumeUnitTests : TestsBase
    {
        public VolumeUnitTests(TestFixture testFixture)
            : base(testFixture) { }

        private VolumeUnit CreateUnitOtherThan(params VolumeUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(VolumeUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
