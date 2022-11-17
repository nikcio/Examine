using Examine.Lucene.Search;
using Lucene.Net.Facet.Range;

namespace Examine.Search
{
    /// <summary>
    /// Represents a long facet field
    /// </summary>
    public interface IFacetLongField : IFacetField
    {
        /// <summary>
        /// The long ranges
        /// </summary>
        Int64Range[] LongRanges { get; }
    }
}
