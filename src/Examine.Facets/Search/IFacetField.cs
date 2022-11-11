using System;

namespace Examine.Facets.Search
{
    public interface IFacetField
    {
        string Name { get; }

        FacetType Type { get; }
    }
}
