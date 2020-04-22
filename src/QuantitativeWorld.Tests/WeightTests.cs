using AutoFixture;
using QuantitativeWorld.TestAbstractions;
using System.Linq;

namespace QuantitativeWorld.Tests
{
    public partial class WeightTests : TestsBase
    {
        public WeightTests(TestFixture testFixture)
            : base(testFixture) { }

        private Weight CreateWeightInUnit(WeightUnit unit) =>
            Fixture
            .Create<Weight>()
            .Convert(unit);

        private Weight CreateWeightInUnitOtherThan(params WeightUnit[] unitsToExclude) =>
            Fixture
            .Create<Weight>()
            .Convert(CreateUnitOtherThan(unitsToExclude));

        private WeightUnit CreateUnitOtherThan(params WeightUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(WeightUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
