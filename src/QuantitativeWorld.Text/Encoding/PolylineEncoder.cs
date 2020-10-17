using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.Text.Encoding
{
    public class PolylineEncoder
    {
        private readonly PolylineEncoding _encoding;

        public PolylineEncoder(PolylineEncoding encoding)
        {
            _encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        public IEnumerable<GeoCoordinate> Decode(string polyline)
        {
            if (polyline == null)
                return null;
            if (polyline == string.Empty)
                return Enumerable.Empty<GeoCoordinate>();

            return
                Decode(polyline.ToCharArray())
                .Select(_encoding.ToGeoCoordinate);
        }

        public string Encode(IEnumerable<GeoCoordinate> coordinates)
        {
            if (coordinates == null)
                return null;
            if (coordinates.IsEmpty())
                return string.Empty;

            var polylineParts =
                new[] { PolylinePoint.Zero }
                .Union(coordinates.Select(_encoding.ToPolylinePoint))
                .Pairwise()
                .Select(e => e.Item2 - e.Item1)
                .Select(Encode);
            return string.Concat(polylineParts);
        }

        private static IEnumerable<PolylinePoint> Decode(char[] polylineChars)
        {
            int index = 0;
            int currentLat = 0;
            int currentLng = 0;
            int next5bits;
            int sum;
            int shifter;

            while (index < polylineChars.Length)
            {
                // calculate next latitude
                sum = 0;
                shifter = 0;
                do
                {
                    next5bits = polylineChars[index++] - 63;
                    sum |= (next5bits & 31) << shifter;
                    shifter += 5;
                } while (next5bits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length)
                    break;

                currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                //calculate next longitude
                sum = 0;
                shifter = 0;
                do
                {
                    next5bits = polylineChars[index++] - 63;
                    sum |= (next5bits & 31) << shifter;
                    shifter += 5;
                } while (next5bits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length && next5bits >= 32)
                    break;

                currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                yield return new PolylinePoint(currentLat, currentLng);
            }
        }

        internal static string Encode(PolylinePoint point)
        {
            var latitudeChars = Encode(point.Latitude);
            var longitudeChars = Encode(point.Longitude);

            int charsCount = latitudeChars.Count + longitudeChars.Count;
            var charsArray = new char[charsCount];
            int index = charsCount - 1;
            while (longitudeChars.Count > 0)
                charsArray[index--] = longitudeChars.Pop();
            while (latitudeChars.Count > 0)
                charsArray[index--] = latitudeChars.Pop();
            return new string(charsArray);
        }

        private static Stack<char> Encode(int value)
        {
            var charsStack = new Stack<char>();
            int shifted = value << 1;
            if (shifted < 0)
                shifted = ~shifted;
            int rem = shifted;
            while (rem >= 0x20)
            {
                charsStack.Push((char)((0x20 | (rem & 0x1f)) + 63));
                rem >>= 5;
            }
            charsStack.Push((char)(rem + 63));
            return charsStack;
        }
    }
}
