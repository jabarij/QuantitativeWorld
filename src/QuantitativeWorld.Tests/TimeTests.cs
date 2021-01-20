using AutoFixture;
using QuantitativeWorld.TestAbstractions;

namespace QuantitativeWorld.Tests.Angular
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public partial class TimeTests : TestsBase
    {
        public TimeTests(TestFixture testFixture)
            : base(testFixture) { }

        private Time CreateTime() =>
            new Time(totalSeconds: Fixture.Create<number>());
    }
}
