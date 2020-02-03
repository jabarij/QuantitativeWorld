using AutoFixture;
using System;

namespace QuantitativeWorld.Tests
{
    public class TestFixture
    {
        public TestFixture()
        {
            // Perform global test setup here
            var fixture = new Fixture();
            CustomizeFixture(fixture);
            Fixture = fixture;
        }

        public IFixture Fixture { get; }
        public decimal DecimalPrecision => 0.0000000000000000000000001m;

        private void CustomizeFixture(Fixture fixture)
        {
            // Customize common fixture setup here
            fixture.Customize<WeightUnit>(e => e.FromFactory(() => fixture.CreateFromSet(WeightUnit.GetKnownUnits())));
            fixture.Customize<Weight>(e => e.FromFactory(() => new Weight(fixture.Create<decimal>(), fixture.Create<WeightUnit>())));

            fixture.Customize<LengthUnit>(e => e.FromFactory(() => fixture.CreateFromSet(LengthUnit.GetKnownUnits())));
            fixture.Customize<Length>(e => e.FromFactory(() => new Length(fixture.Create<decimal>(), fixture.Create<LengthUnit>())));
        }

        private bool _isDisposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            if (disposing)
            {
                Teardown();
                // Dispose managed resources
            }

            _isDisposed = true;
        }

        private void Teardown()
        {
            // Perform global test teardown here
        }
    }
}