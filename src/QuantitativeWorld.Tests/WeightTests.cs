using AutoFixture;
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

    public partial class WeightTests : TestsBase
    {
        public WeightTests(TestFixture testFixture)
            : base(testFixture) { }

        private Weight CreateWeightInUnitOtherThan(params WeightUnit[] unitsToExclude) =>
            Fixture
            .Create<Weight>()
            .Convert(CreateUnitOtherThan(unitsToExclude));

        private WeightUnit CreateUnitOtherThan(params WeightUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(WeightUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
