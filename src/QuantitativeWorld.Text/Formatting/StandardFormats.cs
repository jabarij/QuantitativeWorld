#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Formatting
{
#else
namespace QuantitativeWorld.Text.Formatting
{
#endif
    public static class StandardFormats
    {
        public const string CGS = nameof(CGS);
        public const string IMP = nameof(IMP);
        public const string MKS = nameof(MKS);
        public const string MTS = nameof(MTS);
        public const string SI = nameof(SI);

        internal static bool TryGetUnitSystemByStandardFormat(string standardFormat, out UnitSystem unitSystem)
        {
            switch (standardFormat)
            {
                case SI:
                    unitSystem = UnitSystem.SI;
                    return true;
                case MKS:
                    unitSystem = UnitSystem.MKS;
                    return true;
                case CGS:
                    unitSystem = UnitSystem.CGS;
                    return true;
                case MTS:
                    unitSystem = UnitSystem.MTS;
                    return true;
                case IMP:
                    unitSystem = UnitSystem.IMPERIAL;
                    return true;
                default:
                    unitSystem = null;
                    return false;
            }
        }
    }
}
