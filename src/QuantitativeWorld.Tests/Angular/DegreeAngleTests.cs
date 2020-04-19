using AutoFixture;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;

namespace QuantitativeWorld.Tests.Angular
{
    public partial class DegreeAngleTests : TestsBase
    {
        public DegreeAngleTests(TestFixture testFixture)
            : base(testFixture) { }

        private DegreeAngle CreateDegreeAngle() =>
            new DegreeAngle(totalSeconds: Fixture.Create<double>());
    }
}
