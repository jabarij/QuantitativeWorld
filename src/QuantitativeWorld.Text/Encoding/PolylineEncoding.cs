#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Encoding
{
#else
namespace QuantitativeWorld.Text.Encoding
{
#endif
    public abstract class PolylineEncoding
    {
        public static readonly PolylineEncoding Polyline5 = new Polyline5Encoding();
        public static readonly PolylineEncoding Polyline6 = new Polyline6Encoding();

        internal abstract GeoCoordinate ToGeoCoordinate(PolylinePoint polylinePoint);
        internal abstract PolylinePoint ToPolylinePoint(GeoCoordinate coordinate);
    }
}
