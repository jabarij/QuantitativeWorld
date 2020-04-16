using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System.Linq;

namespace QuantitativeWorld.Tests.Angular
{
    public partial class AngleUnitTests : TestsBase
    {
        public AngleUnitTests(TestFixture testFixture)
            : base(testFixture) { }

        private AngleUnit CreateUnitOtherThan(params AngleUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(AngleUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
