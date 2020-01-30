using System;

namespace DataStructures
{
    public class QuantityConverter<TQuantity, TUnit> : IQuantityConverter<TQuantity, TUnit>
        where TQuantity : IQuantity<TUnit>
        where TUnit : ILinearUnit
    {
        private readonly IQuantityFactory<TQuantity, TUnit> _quantityFactory;
        private readonly IUnitConverter<TUnit> _unitConverter;

        public QuantityConverter(
            IQuantityFactory<TQuantity, TUnit> quantityFactory,
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