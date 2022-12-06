using Lucene.Net.Facet.Range;

namespace Examine.Search
{
    /// <inheritdoc/>
    public class FacetLongField : IFacetLongField
    {
        /// <inheritdoc/>
        public string Field { get; }

        /// <inheritdoc/>
        public Int64Range[] LongRanges { get; }

        /// <inheritdoc/>
        public string FacetField { get; set; } = "$facets";

        /// <inheritdoc/>
        public FacetLongField(string field, Int64Range[] longRanges)
        {
            Field = field;
            LongRanges = longRanges;
        }
    }
}
