using QuantitativeWorld.TestAbstractions;
using System.Linq;

namespace QuantitativeWorld.Tests
{
    public partial class AreaUnitTests : TestsBase
    {
        public AreaUnitTests(TestFixture testFixture)
            : base(testFixture) { }

        private AreaUnit CreateUnitOtherThan(params AreaUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(AreaUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
