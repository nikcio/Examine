using Examine.Search;

namespace Examine.Lucene.Search
{
    /// <summary>
    /// Represents a facet long range field
    /// </summary>
    public class FacetLongRangeQueryField : LuceneBooleanOperation, IFacetLongRangeQueryField
    {
        /// <inheritdoc/>
        public FacetLongRangeQueryField(LuceneSearchQuery search, FacetLongField _) : base(search)
        {
        }
    }
}
