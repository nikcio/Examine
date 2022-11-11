using System;
using System.Collections.Generic;
using Examine.Facets.Search;
using Examine.Lucene;
using Examine.Lucene.Search;
using Examine.Search;
using Lucene.Net.Analysis;
using Lucene.Net.Search;

namespace Examine.Facets.Lucene
{
    /// <summary>
    /// Implements facets for <see cref="IQueryExecutor"/> with Lucene
    /// </summary>
    public class FacetSearchQuery : LuceneSearchQuery, IFacetQuery
    {
        public FacetSearchQuery(ISearchContext searchContext, string category, Analyzer analyzer, LuceneSearchOptions searchOptions, BooleanOperation occurance)
            : base(searchContext, category, analyzer, searchOptions, occurance)
        {

        }

        public IList<IFacetField> Fields { get; } = new List<IFacetField>();

        ///<inheritdoc/>
        public IFacetQueryField Facet(string field) => FacetInternal(field);

        ///<inheritdoc/>
        public IFacetQueryField Facet(string field, string value) => FacetInternal(field, new string[] { value });

        ///<inheritdoc/>
        public IFacetQueryField Facet(string field, string[] values) => FacetInternal(field, values);

        /// <summary>
        /// Register <see cref="IFacetField"/> for use within query
        /// </summary>
        protected virtual IFacetQueryField FacetInternal(string field, string[] values = null)
        {
            var facet = new SortedTextFacetField(field, values);

            Fields.Add(facet);

            return new FacetQueryField(this, facet);
        }

        ///<inheritdoc/>
        protected override LuceneBooleanOperationBase CreateOp() => new FacetBooleanOperation(this);

        /// <inheritdoc />
        public new ISearchResults Execute(QueryOptions options = null) => Search(options);

        ///<inheritdoc/>
        public new ISearchResults Search(QueryOptions options)
        {
            // capture local
            var query = Query;

            if (!string.IsNullOrEmpty(Category))
            {
                // rebuild the query
                IList<BooleanClause> existingClauses = query.Clauses;

                if (existingClauses.Count == 0)
                {
                    // Nothing to search. This can occur in cases where an analyzer for a field doesn't return
                    // anything since it strips all values.
                    return EmptySearchResults.Instance;
                }

                query = new BooleanQuery
                {
                    // prefix the category field query as a must
                    { GetFieldInternalQuery(ExamineFieldNames.CategoryFieldName, new ExamineValue(Examineness.Explicit, Category), true), Occur.MUST }
                };

                // add the ones that we're already existing
                foreach (var c in existingClauses)
                {
                    query.Add(c);
                }
            }

            var executor = new FacetSearchExecutor(options, query, SortFields, _searchContext, _fieldsToLoad, Fields);

            var pagesResults = executor.Execute();

            return pagesResults;
        }
    }
}
