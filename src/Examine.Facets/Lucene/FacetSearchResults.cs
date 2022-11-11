using System;
using System.Collections.Generic;
using Examine.Facets.Search;
using Examine.Lucene;
using Examine.Lucene.Search;
using Lucene.Net.Search;

namespace Examine.Facets.Lucene
{
    public class FacetSearchResults : LuceneSearchResults, IFacetResults
    {
        public static new FacetSearchResults Empty { get; } = new FacetSearchResults(Array.Empty<ISearchResult>(), 0, new Dictionary<string, IFacetResult>());

        public FacetSearchResults(IReadOnlyCollection<ISearchResult> results, int totalItemCount, IDictionary<string, IFacetResult> facets) : base(results, totalItemCount)
        {
            Facets = facets;
        }

        ///<inheritdoc/>
        public IDictionary<string, IFacetResult> Facets { get; }
    }
}
