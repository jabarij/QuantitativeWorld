using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Parsing
{
#else
namespace QuantitativeWorld.Parsing
{
#endif
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = AllowMultiple, Inherited = Inherited)]
    internal class PredefinedAttribute : Attribute
    {
        public const bool Inherited = false;
        public const bool AllowMultiple = false;
    }
}