using AutoFixture;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;

namespace QuantitativeWorld.Tests.Angular
{
    public partial class RadianAngleTests : TestsBase
    {
        public RadianAngleTests(TestFixture testFixture)
            : base(testFixture) { }

        private RadianAngle CreateRadianAngle() =>
            new RadianAngle(radians: Fixture.Create<decimal>());
    }
}
