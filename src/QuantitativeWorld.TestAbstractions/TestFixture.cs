using AutoFixture;
using QuantitativeWorld.Angular;
using System;

namespace QuantitativeWorld.TestAbstractions
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

        private void CustomizeFixture(Fixture fixture)
        {
            // Customize common fixture setup here
            fixture.Customize<double>(e => e.FromFactory(() => fixture.CreateInRange(-100, 100)));

            fixture.Customize<TimeSpan>(e => e.FromFactory(() => TimeSpan.FromSeconds(fixture.Create<double>())));
            fixture.Customize<Time>(e => e.FromFactory(() => new Time(fixture.Create<double>())));

            fixture.Customize<WeightUnit>(e => e.FromFactory(() => fixture.CreateFromSet(WeightUnit.GetPredefinedUnits())));
            fixture.Customize<Weight>(e => e.FromFactory(() => new Weight(fixture.Create<double>()).Convert(fixture.Create<WeightUnit>())));

            fixture.Customize<LengthUnit>(e => e.FromFactory(() => fixture.CreateFromSet(LengthUnit.GetPredefinedUnits())));
            fixture.Customize<Length>(e => e.FromFactory(() => new Length(fixture.Create<double>()).Convert(fixture.Create<LengthUnit>())));

            fixture.Customize<AreaUnit>(e => e.FromFactory(() => fixture.CreateFromSet(AreaUnit.GetPredefinedUnits())));
            fixture.Customize<Area>(e => e.FromFactory(() => new Area(fixture.Create<double>()).Convert(fixture.Create<AreaUnit>())));

            fixture.Customize<VolumeUnit>(e => e.FromFactory(() => fixture.CreateFromSet(VolumeUnit.GetPredefinedUnits())));
            fixture.Customize<Volume>(e => e.FromFactory(() => new Volume(fixture.Create<double>()).Convert(fixture.Create<VolumeUnit>())));

            fixture.Customize<PowerUnit>(e => e.FromFactory(() => fixture.CreateFromSet(PowerUnit.GetPredefinedUnits())));
            fixture.Customize<Power>(e => e.FromFactory(() => new Power(fixture.Create<double>()).Convert(fixture.Create<PowerUnit>())));

            fixture.Customize<AngleUnit>(e => e.FromFactory(() => fixture.CreateFromSet(AngleUnit.GetPredefinedUnits())));
            fixture.Customize<Angle>(e => e.FromFactory(() => new Angle(fixture.Create<double>()).Convert(fixture.Create<AngleUnit>())));

            fixture.Customize<DegreeAngle>(e => e.FromFactory(() => new DegreeAngle(fixture.Create<double>())));
            fixture.Customize<RadianAngle>(e => e.FromFactory(() => new RadianAngle(fixture.Create<double>())));

            fixture.Customize<GeoCoordinate>(e => e.FromFactory(() => new GeoCoordinate(
                latitude: fixture.CreateInRange(GeoCoordinate.MinLatitude, GeoCoordinate.MaxLatitude),
                longitude: fixture.CreateInRange(GeoCoordinate.MinLongitude, GeoCoordinate.MaxLongitude))));
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