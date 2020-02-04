using System;

namespace QuantitativeWorld.Parsing
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = AllowMultiple, Inherited = Inherited)]
    internal class ParsableAttribute : Attribute
    {
        public const bool Inherited = false;
        public const bool AllowMultiple = false;
    }
}