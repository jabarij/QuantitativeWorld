using QuantitativeWorld.TestAbstractions;
using System.Linq;

namespace QuantitativeWorld.Tests
{
    public partial class EnergyUnitTests : TestsBase
    {
        public EnergyUnitTests(TestFixture testFixture)
            : base(testFixture) { }

        private EnergyUnit CreateUnitOtherThan(params EnergyUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(EnergyUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
