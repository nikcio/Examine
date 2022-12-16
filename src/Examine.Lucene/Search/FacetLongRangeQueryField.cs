using Examine.Search;

namespace Examine.Lucene.Search
{
    public class FacetLongRangeQueryField : IFacetLongRangeQueryField
    {
        private readonly LuceneSearchQuery _search;
        private readonly FacetLongField _field;

        public FacetLongRangeQueryField(LuceneSearchQuery search, FacetLongField field)
        {
            _search = search;
            _field = field;
        }

        public IOrdering And() => new LuceneBooleanOperation(_search);
        public ISearchResults Execute(QueryOptions options = null) => _search.Execute(options);

        /// <inheritdoc/>
        public IFacetLongRangeQueryField FacetField(string fieldName)
        {
            _field.FacetField = fieldName;

            return this;
        }
    }
}