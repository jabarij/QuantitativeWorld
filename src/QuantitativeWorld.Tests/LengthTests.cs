using AutoFixture;
using QuantitativeWorld.TestAbstractions;
using System.Linq;

namespace QuantitativeWorld.Tests
{
    public partial class LengthTests : TestsBase
    {
        public LengthTests(TestFixture testFixture)
            : base(testFixture) { }

        private Length CreateLengthInUnit(LengthUnit unit) =>
            Fixture
            .Create<Length>()
            .Convert(unit);

        private Length CreateLengthInUnitOtherThan(params LengthUnit[] unitsToExclude) =>
            Fixture
            .Create<Length>()
            .Convert(CreateUnitOtherThan(unitsToExclude));

        private LengthUnit CreateUnitOtherThan(params LengthUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(LengthUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
