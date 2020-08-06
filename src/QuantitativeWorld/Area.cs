using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    public partial struct Area : ILinearQuantity<AreaUnit>
    {
        private const double MinSquareMetres = double.MinValue;
        private const double MaxSquareMetres = double.MaxValue;

        public static readonly AreaUnit DefaultUnit = AreaUnit.SquareMetre;
        public static readonly Area Zero = new Area(0d);

        private readonly AreaUnit? _formatUnit;

        public Area(double squareMetres)
            : this(formatUnit: null, squareMetres: squareMetres) { }
        public Area(double value, AreaUnit unit)
            : this(formatUnit: unit, squareMetres: GetSquareMetres(value, unit)) { }
        private Area(AreaUnit? formatUnit, double squareMetres)
        {
            Assert.IsInRange(squareMetres, MinSquareMetres, MaxSquareMetres, nameof(squareMetres));

            _formatUnit = formatUnit;
            SquareMetres = squareMetres;
        }

        public double SquareMetres { get; }
        public double Value => GetValue(SquareMetres, Unit);
        public AreaUnit Unit => _formatUnit ?? DefaultUnit;
        double ILinearQuantity<AreaUnit>.BaseValue => SquareMetres;
        AreaUnit ILinearQuantity<AreaUnit>.BaseUnit => DefaultUnit;

        public Area Convert(AreaUnit targetUnit) =>
            new Area(targetUnit, SquareMetres);

        public bool IsZero() =>
            SquareMetres == 0d;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Area, AreaUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Area, AreaUnit>(formatProvider, this);

        private static double GetSquareMetres(double value, AreaUnit sourceUnit) =>
            value * sourceUnit.ValueInSquareMetres;
        private static double GetValue(double metres, AreaUnit targetUnit) =>
            metres / targetUnit.ValueInSquareMetres;
    }
}
