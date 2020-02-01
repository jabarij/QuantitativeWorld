using System;

namespace QuantitativeWorld
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = AllowMultiple, Inherited = Inherited)]
    internal class ParsableAttribute : Attribute
    {
        public const bool Inherited = false;
        public const bool AllowMultiple = false;
    }
}