using System;

namespace QuantitativeWorld
{
    static class Constants
    {
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

        public const double MetresPerMillimetre = 0.001d;
        public const double MetresPerCentimetre = 0.01d;
        public const double MetresPerDecimetre = 0.1d;
        public const double MetresPerKilometre = 1000d;
        public const double MetresPerInch = 0.0254d;
        public const double MetresPerFoot = 0.3048d;
        public const double MetresPerYard = 0.9144d;

        public const double MetresPerNauticalMile = 1852d;

        public const double InchesPerFoot = 12d;
        public const double FeetPerYard = 3d;
        public const double YardsPerRod = 5.5d;
        public const double RodsPerChain = 4d;
        public const double ChainsPerFurlong = 10d;
        public const double FurlongsPerMile = 8d;

        public const double CablesPerNauticalMile = 10d;

        public const double WattsPerMilliwatt = 0.001d;
        public const double WattsPerKilowatt = 1000d;
        public const double WattsPerMegawatt = 1000000d;
        public const double WattsPerMechanicalHorsepower = 76.0402249d * 9.80665d;
        public const double WattsPerMetricHorsepower = 75d * 9.80665d;
        public const double WattsPerErgPerSecond = 0.0000001d;
        public const double WattsPerStheneMetrePerSecond = 1000d;

        public const double RadiansPerTurn = Math.PI;
        public const double DegreesPerTurn = 360d;
        public const double ArcminutesPerTurn = DegreesPerTurn * 60d;
        public const double ArcsecondsPerTurn = ArcminutesPerTurn * 60d;
        public const double GradiansPerTurn = 400d;
        public const double NATOMilsPerTurn = 6400d;
    }
}