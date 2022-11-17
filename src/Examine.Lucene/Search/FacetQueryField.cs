using Examine.Lucene.Search;

namespace Examine.Search
{
    /// <summary>
    /// Represents a default facet query field (FullText)
    /// </summary>
    public class FacetQueryField : LuceneBooleanOperation, IFacetQueryField
    {
        private readonly FacetFullTextField _field;

        /// <inheritdoc/>
        public FacetQueryField(LuceneSearchQuery search, FacetFullTextField field) : base(search)
        {
            _field = field;
        }

        /// <inheritdoc/>
        public IFacetQueryField FacetField(string fieldName)
        {
            _field.FacetField = fieldName;

            return this;
        }

        /// <inheritdoc/>
        public IFacetQueryField MaxCount(int count)
        {
            _field.MaxCount = count;

            return this;
        }
    }
}
