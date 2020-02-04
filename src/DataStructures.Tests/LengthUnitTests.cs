using System.Linq;

namespace QuantitativeWorld.Tests
{
    public partial class LengthUnitTests : TestsBase
    {
        public LengthUnitTests(TestFixture testFixture)
            : base(testFixture) { }

        private LengthUnit CreateUnitOtherThan(params LengthUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(LengthUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
