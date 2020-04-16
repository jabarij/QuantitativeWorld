using QuantitativeWorld.TestAbstractions;
using System.Linq;

namespace QuantitativeWorld.Tests
{
    public partial class PowerUnitTests : TestsBase
    {
        public PowerUnitTests(TestFixture testFixture)
            : base(testFixture) { }

        private PowerUnit CreateUnitOtherThan(params PowerUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(PowerUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
