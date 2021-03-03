using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Conversion
{
    using DecimalQuantitativeWorld.Interfaces;
#else
namespace QuantitativeWorld.Conversion
{
    using QuantitativeWorld.Interfaces;
#endif
    public class LinearQuantityConverter<TQuantity, TUnit> : ILinearQuantityConverter<TQuantity, TUnit>
        where TQuantity : ILinearQuantity<TUnit>
        where TUnit : ILinearUnit
    {
        private readonly ILinearQuantityFactory<TQuantity, TUnit> _quantityFactory;
        private readonly IUnitConverter<TUnit> _unitConverter;

        public LinearQuantityConverter(
            ILinearQuantityFactory<TQuantity, TUnit> quantityFactory,
            IUnitConverter<TUnit> unitConverter)
        {
            _quantityFactory = quantityFactory ?? throw new ArgumentNullException(nameof(quantityFactory));
            _unitConverter = unitConverter ?? throw new ArgumentNullException(nameof(unitConverter));
        }

        public TQuantity Convert(TQuantity quantity, TUnit targetUnit) =>
            _quantityFactory.Create(
                value: _unitConverter.ConvertValue(
                    value: quantity.Value,
                    sourceUnit: quantity.Unit, 
                    targetUnit: targetUnit),
                unit: targetUnit);
    }
}