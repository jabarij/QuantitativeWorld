using System.Linq;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Tests.Angular
{
    using QuantitativeWorld.Angular;
    using QuantitativeWorld.TestAbstractions;
#endif
    public partial class AngleUnitTests : TestsBase
    {
        public AngleUnitTests(TestFixture testFixture)
            : base(testFixture) { }

        private AngleUnit CreateUnitOtherThan(params AngleUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(AngleUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
