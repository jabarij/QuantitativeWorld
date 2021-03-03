using System;

namespace Common.Internals.DotNetExtensions
{
    internal struct DecimalInfo
    {
        public const byte MinExponent = 0;
        public const byte MaxExponent = 28;

        public DecimalInfo(bool isNegative, int mantissaLow, int mantissaMid, int mantissaHigh, byte exponent)
        {
            if (exponent < MinExponent || exponent > MaxExponent)
                throw new ArgumentOutOfRangeException(nameof(exponent), exponent, $"Value must be of range {MinExponent} to {MaxExponent}.");

            IsNegative = isNegative;
            MantissaLow = mantissaLow;
            MantissaMid = mantissaMid;
            MantissaHigh = mantissaHigh;
            Exponent = exponent;
        }

        public bool IsNegative { get; }
        public int MantissaLow { get; }
        public int MantissaMid { get; }
        public int MantissaHigh { get; }
        public byte Exponent { get; }

        public decimal ToDecimal() =>
            new decimal(
                MantissaLow,
                MantissaMid,
                MantissaHigh,
                IsNegative,
                BitConverter.GetBytes(Exponent)[0]);

        public static DecimalInfo FromDecimal(decimal value)
        {
            int[] valueBits = decimal.GetBits(value);
            bool isNegative = valueBits[3] >> 31 == -1;
            byte exponent = (byte)((valueBits[3] << 8) >> 24); // get bits 16-23 out of 32

            return new DecimalInfo(
                isNegative: isNegative,
                mantissaLow: valueBits[0],
                mantissaMid: valueBits[1],
                mantissaHigh: valueBits[2],
                exponent: exponent);
        }
    }
}
