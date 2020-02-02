using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
    public struct MetricPrefix
    {
        public const decimal Base = 10m;

        public static readonly MetricPrefix Yotta = new MetricPrefix(name: "yotta", symbol: "Y", exponent: 24);
        public static readonly MetricPrefix Zetta = new MetricPrefix(name: "zetta", symbol: "Z", exponent: 21);
        public static readonly MetricPrefix Exa = new MetricPrefix(name: "exa", symbol: "E", exponent: 18);
        public static readonly MetricPrefix Peta = new MetricPrefix(name: "peta", symbol: "P", exponent: 15);
        public static readonly MetricPrefix Tera = new MetricPrefix(name: "tera", symbol: "T", exponent: 12);
        public static readonly MetricPrefix Giga = new MetricPrefix(name: "giga", symbol: "G", exponent: 9);
        public static readonly MetricPrefix Mega = new MetricPrefix(name: "mega", symbol: "M", exponent: 6);
        public static readonly MetricPrefix Kilo = new MetricPrefix(name: "kilo", symbol: "k", exponent: 3);
        public static readonly MetricPrefix Hecto = new MetricPrefix(name: "hecto", symbol: "h", exponent: 2);
        public static readonly MetricPrefix Deca = new MetricPrefix(name: "deca", symbol: "da", exponent: 1);

        public static readonly MetricPrefix Deci = new MetricPrefix(name: "deci", symbol: "d", exponent: -1);
        public static readonly MetricPrefix Centi = new MetricPrefix(name: "centi", symbol: "c", exponent: -2);
        public static readonly MetricPrefix Milli = new MetricPrefix(name: "milli", symbol: "m", exponent: -3);
        public static readonly MetricPrefix Micro = new MetricPrefix(name: "micro", symbol: "μ", exponent: -6);
        public static readonly MetricPrefix Nano = new MetricPrefix(name: "nano", symbol: "n", exponent: -9);
        public static readonly MetricPrefix Pico = new MetricPrefix(name: "pico", symbol: "p", exponent: -12);
        public static readonly MetricPrefix Femto = new MetricPrefix(name: "femto", symbol: "f", exponent: -15);
        public static readonly MetricPrefix Atto = new MetricPrefix(name: "atto", symbol: "a", exponent: -18);
        public static readonly MetricPrefix Zepto = new MetricPrefix(name: "zepto", symbol: "z", exponent: -21);
        public static readonly MetricPrefix Yocto = new MetricPrefix(name: "yocto", symbol: "y", exponent: -24);

        private MetricPrefix(string name, string symbol, int exponent)
        {
            Name = Assert.IsNotNullOrWhiteSpace(name, nameof(name));
            Symbol = Assert.IsNotNullOrWhiteSpace(symbol, nameof(symbol));
            Factor = Base.Pow(exponent);
        }

        public string Name { get; }
        public string Symbol { get; }
        public decimal Factor { get; }

        public static MetricPrefix FromExponent(int exponent)
        {
            switch (exponent)
            {
                case -24: return Yocto;
                case -21: return Zepto;
                case -18: return Atto;
                case -15: return Femto;
                case -12: return Pico;
                case -9: return Nano;
                case -6: return Micro;
                case -3: return Milli;
                case -2: return Centi;
                case -1: return Deci;
                case 1: return Deca;
                case 2: return Hecto;
                case 3: return Kilo;
                case 6: return Mega;
                case 9: return Giga;
                case 12: return Tera;
                case 15: return Peta;
                case 18: return Exa;
                case 21: return Zetta;
                case 24: return Yotta;
                default:
                    throw new ArgumentException($"Unknown metric exponent {exponent}.");
            }
        }

        public static LengthUnit operator *(LengthUnit unit, MetricPrefix prefix) =>
            new LengthUnit(
                name: string.Concat(prefix.Name, unit.Name),
                abbreviation: string.Concat(prefix.Symbol, unit.Abbreviation),
                valueInMetres: unit.ValueInMetres * prefix.Factor);
        public static LengthUnit operator *(MetricPrefix prefix, LengthUnit unit) =>
            unit * prefix;

        public static WeightUnit operator *(WeightUnit unit, MetricPrefix prefix) =>
            new WeightUnit(
                name: string.Concat(prefix.Name, unit.Name),
                abbreviation: string.Concat(prefix.Symbol, unit.Abbreviation),
                valueInKilograms: unit.ValueInKilograms * prefix.Factor);
        public static WeightUnit operator *(MetricPrefix prefix, WeightUnit unit) =>
            unit * prefix;
    }
}