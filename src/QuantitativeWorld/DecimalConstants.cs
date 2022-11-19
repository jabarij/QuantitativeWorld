#if DECIMAL
namespace DecimalQuantitativeWorld
#else
namespace QuantitativeWorld
#endif
{
    static class DecimalConstants
    {
        public const decimal MinusOne = -1m;
        public const decimal Zero = 0m;
        public const decimal Half = 0.5m;
        public const decimal Quarter = 0.25m;
        public const decimal One = 1m;
        public const decimal OneThousandth = 0.001m;
        public const decimal Ten = 10m;
        public const decimal TenThousand = 10000m;
        public const decimal TenThousandth = 0.0001m;
        public const decimal OneMillionth = 0.000001m;
        public const decimal Thousand = 1000m;
        public const decimal Million = 1000000m;
        public const decimal Billion = 1000000000m;
        public const decimal PI = 3.1415926535897932384626433833m;

        #region Time

        public const decimal SecondsPerMinute = 60m;
        public const decimal SecondsPerHour = 3600m;
        public const decimal MinutesPerHour = 60m;

        #endregion

        #region Weight

        public const decimal KilogramsPerGram = 0.001m;
        public const decimal KilogramsPerQuintal = 100m;
        public const decimal KilogramsPerTon = 1000m;
        public const decimal MilligramsPerGram = 1000m;
        public const decimal MicrogramsPerGram = 1000000m;
        public const decimal GramsPerDecagram = 10m;

        public const decimal KilogramsPerPound = 0.45359237m;
        public const decimal OuncesPerPound = 16m;
        public const decimal GrainsPerTroyOunce = 480m;
        public const decimal GrainsPerTroyPound = 5760m;
        public const decimal GrainsPerPound = 7000m;
        public const decimal PoundsPerStone = 14m;
        public const decimal PoundsPerCental = 100m;
        public const decimal StonesPerHundredweight = 8m;

        #endregion

        #region Length

        public const decimal MetresPerMillimetre = 0.001m;
        public const decimal MetresPerCentimetre = 0.01m;
        public const decimal MetresPerDecimetre = 0.1m;
        public const decimal MetresPerKilometre = 1000m;
        public const decimal MetresPerInch = 0.0254m;

        public const decimal MetresPerNauticalMile = 1852m;

        public const decimal InchesPerFoot = 12m;
        public const decimal FeetPerYard = 3m;
        public const decimal YardsPerRod = 5.5m;
        public const decimal RodsPerChain = 4m;
        public const decimal ChainsPerFurlong = 10m;
        public const decimal FurlongsPerMile = 8m;
        public const decimal MilesPerLeague = 3m;

        public const decimal YardsPerFathom = 2m;
        public const decimal FathomsPerCable = 120m;
        public const decimal CablesPerNauticalMile = 10m;

        #endregion

        #region Speed

        public const decimal MetresPerSecondPerKilometrePerHour = 1m / 3.6m;
        public const decimal MetresPerSecondPerFootPerSecond = 0.3048m;
        public const decimal MetresPerSecondPerMilePerHour = 1609.344m / 3600m;

        #endregion

        #region Area

        public const decimal SquareMetresPerSquareMillimetre = 0.000001m;
        public const decimal SquareMetresPerSquareCentimetre = 0.0001m;
        public const decimal SquareMetresPerSquareDecimetre = 0.01m;
        public const decimal SquareMetresPerSquareKilometre = 1000000m;
        public const decimal SquareMetresPerSquareInch = 0.00064516m;

        public const decimal SquareMetresPerAre = 100m;
        public const decimal AresPerHectare = 100m;

        public const decimal SquareInchesPerSquareFoot = 144m;
        public const decimal SquareFeetPerSquareYard = 9m;
        public const decimal SquareYardsPerSquareRod = 30.25m;
        public const decimal SquareRodsPerSquareChain = 16m;
        public const decimal SquareChainsPerSquareFurlong = 100m;
        public const decimal SquareFurlongsPerSquareMile = 64m;
        public const decimal SquareMilesPerSquareLeague = 9m;

        public const decimal SquareYardsPerAcre = 4840m;

        #endregion

        #region Volume

        public const decimal CubicMetresPerCubicMillimetre = 0.000000001m;
        public const decimal CubicMetresPerCubicCentimetre = 0.000001m;
        public const decimal CubicMetresPerCubicDecimetre = 0.001m;
        public const decimal CubicMetresPerCubicKilometre = 1000000000m;
        public const decimal CubicMetresPerCubicInch = 0.000016387064m;

        public const decimal CubicInchesPerCubicFoot = 1728m;
        public const decimal CubicFeetPerCubicYard = 27m;
        public const decimal CubicYardsPerCubicRod = 166.375m;
        public const decimal CubicRodsPerCubicChain = 64m;
        public const decimal CubicChainsPerCubicFurlong = 1000m;
        public const decimal CubicFurlongsPerCubicMile = 512m;
        public const decimal CubicMilesPerCubicLeague = 27m;

        public const decimal CubicMetresPerLitre = 0.001m;
        public const decimal MillilitresPerLitre = 1000m;
        public const decimal CentilitresPerLitre = 100m;
        public const decimal DecilitresPerLitre = 10m;
        public const decimal HectolitresPerLitre = 0.01m;

        public const decimal ImperialGallonsPerCubicMetre = 0.00454609m;
        public const decimal ImperialQuartsPerImperialGallon = 4m;
        public const decimal ImperialPintsPerImperialGallon = 8m;

        public const decimal USLiquidGallonsPerCubicMetre = 0.003785411784m;
        public const decimal USDryGallonsPerCubicMetre = 0.00440488377086m;

        public const decimal USLiquidGallonsPerOilBarrel = 42m;

        #endregion

        #region Power

        public const decimal WattsPerMilliwatt = 0.001m;
        public const decimal WattsPerKilowatt = 1000m;
        public const decimal WattsPerMegawatt = 1000000m;
        public const decimal WattsPerMechanicalHorsepower = 76.0402249m * 9.80665m;
        public const decimal WattsPerMetricHorsepower = 75m * 9.80665m;
        public const decimal WattsPerErgPerSecond = 0.0000001m;
        public const decimal WattsPerStheneMetrePerSecond = 1000m;

        #endregion

        #region Angles

        public const decimal ArcminutesPerDegree = 60m;
        public const decimal ArcsecondsPerArcminute = 60m;
        public const decimal ArcsecondsPerDegree = ArcsecondsPerArcminute * ArcminutesPerDegree;

        public const decimal RadiansPerTurn = PI;
        public const decimal DegreesPerTurn = 360m;
        public const decimal ArcminutesPerTurn = DegreesPerTurn * ArcminutesPerDegree;
        public const decimal ArcsecondsPerTurn = ArcminutesPerTurn * ArcsecondsPerArcminute;
        public const decimal GradiansPerTurn = 400m;
        public const decimal NATOMilsPerTurn = 6400m;

        #endregion

        #region Energy

        public const decimal JoulesPerKilojoule = 1000m;
        public const decimal JoulesPerMegajoule = 1000000m;

        public const decimal JoulesPerWattSecond = 1m;
        public const decimal JoulesPerWattHour = 3600m;
        public const decimal JoulesPerKilowattHour = 3600m;
        public const decimal JoulesPerMegawattHour = 3600000m;

        public const decimal JoulesPerElectronvolt = 1.602176634E-19m;
        public const decimal JoulesPerFootPoundForce = 1.3558179483314004m;
        public const decimal JoulesPerErg = 1E-7m;
        public const decimal JoulesPerStheneMetre = 1000m;

        public const decimal JoulesPerCalorie = 4.184m;
        public const decimal JoulesPerKilocalorie = 4184m;

        #endregion
    }
}