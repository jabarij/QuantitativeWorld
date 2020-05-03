using System;

namespace QuantitativeWorld
{
    static class Constants
    {
        #region Weight

        public const double KilogramsPerGram = 0.001d;
        public const double KilogramsPerQuintal = 100d;
        public const double KilogramsPerTon = 1000d;
        public const double MilligramsPerGram = 1000d;
        public const double GramsPerDecagram = 10d;

        public const double KilogramsPerPound = 0.45359237d;
        public const double OuncesPerPound = 16d;
        public const double PoundsPerStone = 14d;
        public const double PoundsPerCental = 100d;
        public const double StonesPerHundredweight = 8d;

        #endregion

        #region Length

        public const double MetresPerMillimetre = 0.001d;
        public const double MetresPerCentimetre = 0.01d;
        public const double MetresPerDecimetre = 0.1d;
        public const double MetresPerKilometre = 1000d;
        public const double MetresPerInch = 0.0254d;

        public const double MetresPerNauticalMile = 1852d;

        public const double InchesPerFoot = 12d;
        public const double FeetPerYard = 3d;
        public const double YardsPerRod = 5.5d;
        public const double RodsPerChain = 4d;
        public const double ChainsPerFurlong = 10d;
        public const double FurlongsPerMile = 8d;
        public const double MilesPerLeague = 3d;

        public const double YardsPerFathom = 2d;
        public const double FathomsPerCable = 120d;
        public const double CablesPerNauticalMile = 10d;

        #endregion

        #region Area

        public const double SquareMetresPerSquareMillimetre = 0.000001d;
        public const double SquareMetresPerSquareCentimetre = 0.0001d;
        public const double SquareMetresPerSquareDecimetre = 0.01d;
        public const double SquareMetresPerSquareKilometre = 1000000d;
        public const double SquareMetresPerSquareInch = 0.00064516d;

        public const double SquareMetresPerAre = 100d;
        public const double AresPerHectare = 100d;

        public const double SquareInchesPerSquareFoot = 144d;
        public const double SquareFeetPerSquareYard = 9d;
        public const double SquareYardsPerSquareMile = 3097600d;

        public const double SquareYardsPerAcre = 4840d;

        #endregion

        #region Power

        public const double WattsPerMilliwatt = 0.001d;
        public const double WattsPerKilowatt = 1000d;
        public const double WattsPerMegawatt = 1000000d;
        public const double WattsPerMechanicalHorsepower = 76.0402249d * 9.80665d;
        public const double WattsPerMetricHorsepower = 75d * 9.80665d;
        public const double WattsPerErgPerSecond = 0.0000001d;
        public const double WattsPerStheneMetrePerSecond = 1000d;

        #endregion

        #region Angles

        public const double ArcminutesPerDegree = 60d;
        public const double ArcsecondsPerArcminute = 60d;
        public const double ArcsecondsPerDegree = ArcsecondsPerArcminute * ArcminutesPerDegree;

        public const double RadiansPerTurn = Math.PI;
        public const double DegreesPerTurn = 360d;
        public const double ArcminutesPerTurn = DegreesPerTurn * ArcminutesPerDegree;
        public const double ArcsecondsPerTurn = ArcminutesPerTurn * ArcsecondsPerArcminute;
        public const double GradiansPerTurn = 400d;
        public const double NATOMilsPerTurn = 6400d;

        #endregion
    }
}