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

    public partial class LengthUnitTests : TestsBase
    {
        public LengthUnitTests(TestFixture testFixture)
            : base(testFixture) { }

        private LengthUnit CreateUnitOtherThan(params LengthUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(LengthUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
