using AutoFixture;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System.Linq;

namespace QuantitativeWorld.Tests.Angular
{
    public partial class AngleTests : TestsBase
    {
        public AngleTests(TestFixture testFixture)
            : base(testFixture) { }

        private Angle CreateAngleInUnit(AngleUnit unit) =>
            Fixture
            .Create<Angle>()
            .Convert(unit);

        private Angle CreateAngleInUnitOtherThan(params AngleUnit[] unitsToExclude) =>
            Fixture
            .Create<Angle>()
            .Convert(CreateUnitOtherThan(unitsToExclude));

        private AngleUnit CreateUnitOtherThan(params AngleUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(AngleUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
