using System;

namespace Examine.Facets.Search
{
    public abstract class FacetField : IFacetField
    {
        protected FacetField(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public abstract FacetType Type { get; }
    }
}
