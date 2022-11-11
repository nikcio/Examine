using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Examine.Facets;
using Examine.Facets.Lucene;
using Examine.Facets.Search;
using Examine.Lucene.Search;
using Examine.Search;
using Lucene.Net.Facet;
using Lucene.Net.Facet.Range;
using Lucene.Net.Facet.SortedSet;
using Lucene.Net.Search;

namespace Examine.Lucene
{
    public class FacetSearchExecutor : LuceneSearchExecutor
    {
        private readonly IList<IFacetField> _fields;

        protected internal FacetSearchExecutor(QueryOptions options, Query query, IEnumerable<SortField> sortField, ISearchContext searchContext, ISet<string> fieldsToLoad, IList<IFacetField> fields) : base(options, query, sortField, searchContext, fieldsToLoad)
        {
            _fields = fields;
        }

        protected override ISearchResults ExecuteSearch(ICollector topDocsCollector, SortField[] sortFields)
        {
            using (ISearcherReference searcher = _searchContext.GetSearcher())
            {
                var facetsCollector = new FacetsCollector();
                searcher.IndexSearcher.Search(_luceneQuery, MultiCollector.Wrap(topDocsCollector, facetsCollector));

                GetSearchResults(topDocsCollector, sortFields, searcher, out int totalItemCount, out List<ISearchResult> results);

                SortedSetDocValuesReaderState state = new DefaultSortedSetDocValuesReaderState(searcher.IndexSearcher.IndexReader);

                var facets = new Dictionary<string, IFacetResult>();

                foreach(var field in _fields)
                {
                    if(field is SortedTextFacetField sortedTextFacetField)
                    {
                        var sortedFacetsCounts = new SortedSetDocValuesFacetCounts(state, facetsCollector);

                        if(sortedTextFacetField.Values != null && sortedTextFacetField.Values.Length > 0)
                        {
                            var facetValues = new List<FacetValue>();
                            foreach(var label in sortedTextFacetField.Values)
                            {
                                var value = sortedFacetsCounts.GetSpecificValue(sortedTextFacetField.Name, label);
                                facetValues.Add(new FacetValue(label, value));
                            }
                            facets.Add(sortedTextFacetField.Name, new Facets.FacetResult(facetValues.OrderBy(value => value.Value).Take(sortedTextFacetField.MaxCount)));
                        }
                        else
                        {
                            var sortedFacets = sortedFacetsCounts.GetTopChildren(sortedTextFacetField.MaxCount, sortedTextFacetField.Name);
                            facets.Add(sortedTextFacetField.Name, new Facets.FacetResult(sortedFacets.LabelValues.Select(labelValue => new FacetValue(labelValue.Label, labelValue.Value))));
                        }
                        
                    }
                    
                }
                //var doubleFacets = new DoubleRangeFacetCounts(field, facetsCollector);
                //doubleFacets.GetSpecificValue()
                //var facets = new SortedSetDocValuesFacetCounts(state, facetsCollector);
                //facets.
                //var doubleFacets = new DoubleRangeFacetCounts(field, facetsCollector);
                //var longFacets = new Int64RangeFacetCounts(field, facetsCollector);
                //facets.

                //var facets = 

                return new FacetSearchResults(results, totalItemCount, facets);
            }
        }
    }
}
