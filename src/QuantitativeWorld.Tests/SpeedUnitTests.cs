using QuantitativeWorld.TestAbstractions;
using System.Linq;

namespace QuantitativeWorld.Tests
{
    public partial class SpeedUnitTests : TestsBase
    {
        public SpeedUnitTests(TestFixture testFixture)
            : base(testFixture) { }

        private SpeedUnit CreateUnitOtherThan(params SpeedUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(SpeedUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
