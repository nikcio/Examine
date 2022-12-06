using Examine.Lucene.Search;

namespace Examine.Search
{
    /// <summary>
    /// Representsa full text facet field
    /// </summary>
    public interface IFacetFullTextField : IFacetField
    {
        /// <summary>
        /// Maximum number of terms to return
        /// </summary>
        int MaxCount { get; set; }

        /// <summary>
        /// Filter values
        /// </summary>
        string[] Values { get; set; }
    }
}
