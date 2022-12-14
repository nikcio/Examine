using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Examine.Lucene.Search
{
    public class LuceneSearchResults : ILuceneSearchResults, IFacetResults
    {
        public static LuceneSearchResults Empty { get; } = new LuceneSearchResults(Array.Empty<ISearchResult>(), 0,float.NaN, default, new ReadOnlyDictionary<string, IFacetResult>(new Dictionary<string, IFacetResult>(0)));

        private readonly IReadOnlyCollection<ISearchResult> _results;

        public LuceneSearchResults(IReadOnlyCollection<ISearchResult> results, int totalItemCount, float maxScore, SearchAfterOptions searchAfterOptions, IReadOnlyDictionary<string, IFacetResult> facets)
        {
            _results = results;
            TotalItemCount = totalItemCount;
            MaxScore = maxScore;
            SearchAfter = searchAfterOptions;
            Facets = facets;
            Facets = facets;
        }

        public long TotalItemCount { get; }

        /// <summary>
        /// Returns the maximum score value encountered. Note that in case
        /// scores are not tracked, this returns <see cref="float.NaN"/>.
        /// </summary>
        public float MaxScore { get; }

        public SearchAfterOptions SearchAfter { get; }

        public IReadOnlyDictionary<string, IFacetResult> Facets { get; }

        public IEnumerator<ISearchResult> GetEnumerator() => _results.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
