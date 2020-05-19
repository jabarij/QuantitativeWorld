using AutoFixture;
using QuantitativeWorld.TestAbstractions;

namespace QuantitativeWorld.Tests.Angular
{
    public partial class TimeTests : TestsBase
    {
        public TimeTests(TestFixture testFixture)
            : base(testFixture) { }

        private Time CreateTime() =>
            new Time(totalSeconds: Fixture.Create<double>());
    }
}
