using Plant.QAM.BusinessLogic.PublishedLanguage;

namespace DataStructures
{
    public struct Quantity<TUnit> : IQuantity<TUnit>
        where TUnit : ILinearUnit
    {
        public Quantity(decimal value, TUnit unit)
        {
            Assert.IsNotNull(unit, nameof(unit));
            Value = value;
            Unit = unit;
        }

        public decimal Value { get; }
        public TUnit Unit { get; }
    }
}