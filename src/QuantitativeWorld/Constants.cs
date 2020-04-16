using System;

namespace QuantitativeWorld
{
    static class Constants
    {
        public const decimal KilogramsPerGram = 0.001m;
        public const decimal KilogramsPerQuintal = 100m;
        public const decimal KilogramsPerTon = 1000m;
        public const decimal MilligramsPerGram = 1000m;
        public const decimal GramsPerDecagram = 10m;

        public const decimal KilogramsPerPound = 0.45359237m;
        public const decimal OuncesPerPound = 16m;
        public const decimal PoundsPerStone = 14m;
        public const decimal PoundsPerCental = 100m;
        public const decimal StonesPerHundredweight = 8m;

        public const decimal MetresPerMillimetre = 0.001m;
        public const decimal MetresPerCentimetre = 0.01m;
        public const decimal MetresPerDecimetre = 0.1m;
        public const decimal MetresPerKilometre = 1000m;
        public const decimal MetresPerInch = 0.0254m;
        public const decimal MetresPerFoot = 0.3048m;
        public const decimal MetresPerYard = 0.9144m;

        public const decimal MetresPerNauticalMile = 1852m;

        public const decimal InchesPerFoot = 12m;
        public const decimal FeetPerYard = 3m;
        public const decimal YardsPerRod = 5.5m;
        public const decimal RodsPerChain = 4m;
        public const decimal ChainsPerFurlong = 10m;
        public const decimal FurlongsPerMile = 8m;

        public const decimal CablesPerNauticalMile = 10m;

        public const decimal WattsPerMilliwatt = 0.001m;
        public const decimal WattsPerKilowatt = 1000m;
        public const decimal WattsPerMegawatt = 1000000m;
        public const decimal WattsPerMechanicalHorsepower = 76.0402249m * 9.80665m;
        public const decimal WattsPerMetricHorsepower = 75m * 9.80665m;
        public const decimal WattsPerErgPerSecond = 0.0000001m;
        public const decimal WattsPerStheneMetrePerSecond = 1000m;

        public const decimal RadiansPerTurn = (decimal)Math.PI;
        public const decimal DegreesPerTurn = 360m;
        public const decimal ArcminutesPerTurn = DegreesPerTurn * 60m;
        public const decimal ArcsecondsPerTurn = ArcminutesPerTurn * 60m;
        public const decimal GradiansPerTurn = 400m;
        public const decimal NATOMilsPerTurn = 6400m;
    }
}