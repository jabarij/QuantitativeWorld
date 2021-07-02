using System;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
#else
namespace QuantitativeWorld
{
#endif
    static class DoubleConstants
    {
        public const double MinusOne = -1d;
        public const double Zero = 0d;
        public const double Half = 0.5d;
        public const double Quarter = 0.25d;
        public const double One = 1d;
        public const double OneThousandth = 0.001d;
        public const double Ten = 10d;
        public const double TenThousand = 10000d;
        public const double TenThousandth = 0.0001d;
        public const double OneMillionth = 0.000001d;
        public const double Thousand = 1000d;
        public const double Million = 1000000d;
        public const double Billion = 1000000000d;
        public const double PI = Math.PI;

        #region Time

        public const double SecondsPerMinute = 60d;
        public const double SecondsPerHour = 3600d;
        public const double MinutesPerHour = 60d;

        #endregion

        #region Weight

        public const double KilogramsPerGram = 0.001d;
        public const double KilogramsPerQuintal = 100d;
        public const double KilogramsPerTon = 1000d;
        public const double MilligramsPerGram = 1000d;
        public const double MicrogramsPerGram = 1000000d;
        public const double GramsPerDecagram = 10d;

        public const double KilogramsPerPound = 0.45359237d;
        public const double OuncesPerPound = 16d;
        public const double GrainsPerTroyOunce = 480d;
        public const double GrainsPerTroyPound = 5760d;
        public const double GrainsPerPound = 7000d;
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

        #region Speed

        public const double MetresPerSecondPerKilometrePerHour = 1d / 3.6d;
        public const double MetresPerSecondPerFootPerSecond = 0.3048d;
        public const double MetresPerSecondPerMilePerHour = 1609.344d / 3600d;

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
        public const double SquareYardsPerSquareRod = 30.25d;
        public const double SquareRodsPerSquareChain = 16d;
        public const double SquareChainsPerSquareFurlong = 100d;
        public const double SquareFurlongsPerSquareMile = 64d;
        public const double SquareMilesPerSquareLeague = 9d;

        public const double SquareYardsPerAcre = 4840d;

        #endregion

        #region Volume

        public const double CubicMetresPerCubicMillimetre = 0.000000001d;
        public const double CubicMetresPerCubicCentimetre = 0.000001d;
        public const double CubicMetresPerCubicDecimetre = 0.001d;
        public const double CubicMetresPerCubicKilometre = 1000000000d;
        public const double CubicMetresPerCubicInch = 0.000016387064d;

        public const double CubicInchesPerCubicFoot = 1728d;
        public const double CubicFeetPerCubicYard = 27d;
        public const double CubicYardsPerCubicRod = 166.375d;
        public const double CubicRodsPerCubicChain = 64d;
        public const double CubicChainsPerCubicFurlong = 1000d;
        public const double CubicFurlongsPerCubicMile = 512d;
        public const double CubicMilesPerCubicLeague = 27d;

        public const double CubicMetresPerLitre = 0.001d;
        public const double MillilitresPerLitre = 1000d;
        public const double CentilitresPerLitre = 100d;
        public const double DecilitresPerLitre = 10d;
        public const double HectolitresPerLitre = 0.01d;

        public const double ImperialGallonsPerCubicMetre = 0.00454609d;
        public const double ImperialQuartsPerImperialGallon = 4d;
        public const double ImperialPintsPerImperialGallon = 8d;

        public const double USLiquidGallonsPerCubicMetre = 0.003785411784d;
        public const double USDryGallonsPerCubicMetre = 0.00440488377086d;

        public const double USLiquidGallonsPerOilBarrel = 42d;

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

        public const double RadiansPerTurn = PI;
        public const double DegreesPerTurn = 360d;
        public const double ArcminutesPerTurn = DegreesPerTurn * ArcminutesPerDegree;
        public const double ArcsecondsPerTurn = ArcminutesPerTurn * ArcsecondsPerArcminute;
        public const double GradiansPerTurn = 400d;
        public const double NATOMilsPerTurn = 6400d;

        #endregion

        #region Energy

        public const double JoulesPerKilojoule = 1000d;
        public const double JoulesPerMegajoule = 1000000d;

        public const double JoulesPerWattSecond = 1d;
        public const double JoulesPerWattHour = 3600d;
        public const double JoulesPerKilowattHour = 3600000d;
        public const double JoulesPerMegawattHour = 3600000000d;

        public const double JoulesPerElectronvolt = 1.602176634E-19d;
        public const double JoulesPerFootPoundForce = 1.3558179483314004d;
        public const double JoulesPerErg = 1E-7d;
        public const double JoulesPerStheneMetre = 1000d;

        public const double JoulesPerCalorie = 4.184d;
        public const double JoulesPerKilocalorie = 4184d;

        #endregion
    }
}