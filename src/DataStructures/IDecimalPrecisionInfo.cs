using System;

namespace DataStructures
{
    public interface IDecimalPrecisionInfo
    {
        int Decimals { get; }
        MidpointRounding Mode { get; }
    }
}