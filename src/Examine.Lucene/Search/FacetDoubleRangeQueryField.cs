using Examine.Search;

namespace Examine.Lucene.Search
{
    /// <summary>
    /// Represents a facet double range query field
    /// </summary>
    public class FacetDoubleRangeQueryField : LuceneBooleanOperation, IFacetDoubleRangeQueryField
    {
        private readonly FacetDoubleField _field;

        /// <inheritdoc/>
        public FacetDoubleRangeQueryField(LuceneSearchQuery search, FacetDoubleField field) : base(search)
        {
            _field = field;
        }

        /// <inheritdoc/>
        public IFacetDoubleRangeQueryField IsFloat(bool isFloat)
        {
            _field.IsFloat = isFloat;

            return this;
        }
    }
}
