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

    public partial class WeightUnitTests : TestsBase
    {
        public WeightUnitTests(TestFixture testFixture)
            : base(testFixture) { }

        private WeightUnit CreateUnitOtherThan(params WeightUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(WeightUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
