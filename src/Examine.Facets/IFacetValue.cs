using System;

namespace Examine.Facets
{
    public interface IFacetValue
    {
        string Label { get; }

        float Value { get; }
    }
}
