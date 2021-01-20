using AutoFixture;
using QuantitativeWorld.Angular;
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

    public partial class DegreeAngleTests : TestsBase
    {
        public DegreeAngleTests(TestFixture testFixture)
            : base(testFixture) { }

        private DegreeAngle CreateDegreeAngle() =>
            new DegreeAngle(totalSeconds: Fixture.Create<number>());
    }
}
