using System.Collections.Generic;

namespace Examine.Lucene.Search
{
    /// <summary>
    /// Represents a search result containing facets
    /// </summary>
    public interface IFacetResults
    {
        /// <summary>
        /// Facets from the search
        /// </summary>
        IDictionary<string, IFacetResult> Facets { get; }
    }
}
