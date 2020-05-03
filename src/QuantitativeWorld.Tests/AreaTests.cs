using AutoFixture;
using QuantitativeWorld.TestAbstractions;
using System.Linq;

namespace QuantitativeWorld.Tests
{
    public partial class AreaTests : TestsBase
    {
        public AreaTests(TestFixture testFixture)
            : base(testFixture) { }

        private Area CreateAreaInUnit(AreaUnit unit) =>
            Fixture
            .Create<Area>()
            .Convert(unit);

        private Area CreateAreaInUnitOtherThan(params AreaUnit[] unitsToExclude) =>
            Fixture
            .Create<Area>()
            .Convert(CreateUnitOtherThan(unitsToExclude));

        private AreaUnit CreateUnitOtherThan(params AreaUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(AreaUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
