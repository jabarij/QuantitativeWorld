using System;
using System.Collections.Generic;
using System.Text;

namespace QuantitativeWorld.Interfaces
{
    public interface INamedUnit
    {
        string Name { get; }
        string Abbreviation { get; }
    }
}
