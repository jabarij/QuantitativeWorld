using Plant.QAM.BusinessLogic.PublishedLanguage;
using System;

namespace DataStructures
{
    public class DecimalPrecisionInfo: IDecimalPrecisionInfo
    {
        public const int MinDecimals = 0;
        public const int MaxDecimals = 28;

        public DecimalPrecisionInfo(int decimals = MaxDecimals, MidpointRounding mode = MidpointRounding.AwayFromZero)
        {
            Assert.IsInRange(decimals, MinDecimals, MaxDecimals, nameof(decimals));
            Decimals = decimals;
            Mode = mode;
        }

        public int Decimals { get; }
        public MidpointRounding Mode { get; }

        public static IDecimalPrecisionInfo GetInstance(IFormatProvider formatProvider)
        {
            Assert.IsNotNull(formatProvider, nameof(formatProvider));

            var precisionInfo = (IDecimalPrecisionInfo)formatProvider.GetFormat(typeof(IDecimalPrecisionInfo));
            if (precisionInfo != null)
                return precisionInfo;

            return new DecimalPrecisionInfo();
        }
    }
}