using Plant.QAM.BusinessLogic.PublishedLanguage;
using Plant.QAM.BusinessLogic.PublishedLanguage.Monads;
using System;

namespace DataStructures
{
    public class DecimalPrecisionManager : IDecimalPrecisionManager
    {
        private readonly Maybe<(int, MidpointRounding)> _rounding;

        public DecimalPrecisionManager()
        {
            _rounding = Maybe.None<(int, MidpointRounding)>();
        }
        public DecimalPrecisionManager(int decimals, MidpointRounding mode)
        {
            Assert.IsInRange(decimals, 0, 28, nameof(decimals));
            _rounding = Maybe.Some((decimals, mode));
        }

        public decimal Round(decimal value) =>
            _rounding.Match(
                mapLeft: e => Round(value, e.Item1, e.Item2),
                mapRight: _ => value);

        private decimal Round(decimal value, int decimals, MidpointRounding mode) =>
            Math.Round(value, decimals, mode);

        public static IDecimalPrecisionManager GetInstance(IFormatProvider formatProvider)
        {
            Assert.IsNotNull(formatProvider, nameof(formatProvider));

            var precisionManager = (IDecimalPrecisionManager)formatProvider.GetFormat(typeof(IDecimalPrecisionManager));
            if (precisionManager != null)
                return precisionManager;

            var precisionInfo = (IDecimalPrecisionInfo)formatProvider.GetFormat(typeof(IDecimalPrecisionInfo));
            if (precisionInfo != null)
                return new DecimalPrecisionManager(precisionInfo.Decimals, precisionInfo.Mode);

            return new DecimalPrecisionManager();
        }
    }
}