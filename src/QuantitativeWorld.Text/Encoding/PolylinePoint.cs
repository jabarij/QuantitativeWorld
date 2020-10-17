namespace QuantitativeWorld.Text.Encoding
{
    internal struct PolylinePoint
    {
        public static readonly PolylinePoint Zero = new PolylinePoint();

        public PolylinePoint(int latitude, int longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public readonly int Latitude;
        public readonly int Longitude;

        public static PolylinePoint operator -(PolylinePoint left, PolylinePoint right) =>
            new PolylinePoint(
                latitude: left.Latitude - right.Latitude,
                longitude: left.Longitude - right.Longitude);
        public static PolylinePoint operator <<(PolylinePoint point, int bytes) =>
            new PolylinePoint(
                latitude: point.Latitude << bytes,
                longitude: point.Longitude << bytes);
    }
}
