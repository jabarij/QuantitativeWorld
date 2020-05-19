using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld
{
    public partial struct Energy : ILinearQuantity<EnergyUnit>
    {
        private const double MinJoules = double.MinValue;
        private const double MaxJoules = double.MaxValue;

        public static readonly EnergyUnit DefaultUnit = EnergyUnit.Joule;

        private readonly EnergyUnit? _formatUnit;

        public Energy(double joules)
            : this(formatUnit: null, joules: joules) { }
        public Energy(double value, EnergyUnit unit)
            : this(formatUnit: unit, joules: GetJoules(value, unit)) { }
        private Energy(EnergyUnit? formatUnit, double joules)
        {
            Assert.IsInRange(joules, MinJoules, MaxJoules, nameof(joules));

            _formatUnit = formatUnit;
            Joules = joules;
        }

        public double Joules { get; }
        public double Value => GetValue(Joules, Unit);
        public EnergyUnit Unit => _formatUnit ?? DefaultUnit;
        double ILinearQuantity<EnergyUnit>.BaseValue => Joules;
        EnergyUnit ILinearQuantity<EnergyUnit>.BaseUnit => DefaultUnit;

        public Energy Convert(EnergyUnit targetUnit) =>
            new Energy(targetUnit, Joules);

        public bool IsZero() =>
            Joules == 0d;

        public override string ToString() =>
            DummyStaticFormatter.ToString<Energy, EnergyUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<Energy, EnergyUnit>(formatProvider, this);

        private static double GetJoules(double value, EnergyUnit sourceUnit) =>
            value * sourceUnit.ValueInJoules;
        private static double GetValue(double joules, EnergyUnit targetUnit) =>
            joules / targetUnit.ValueInJoules;
    }
}
