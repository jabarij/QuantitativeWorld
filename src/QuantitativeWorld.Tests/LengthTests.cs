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
            new Length(
                value: Fixture.Create<decimal>(),
                unit: unit);

        private Length CreateLengthInUnitOtherThan(params LengthUnit[] unitsToExclude) =>
            new Length(
                value: Fixture.Create<decimal>(),
                unit: CreateUnitOtherThan(unitsToExclude));

        private LengthUnit CreateUnitOtherThan(params LengthUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(LengthUnit.GetPredefinedUnits().Except(unitsToExclude));
    }
}
