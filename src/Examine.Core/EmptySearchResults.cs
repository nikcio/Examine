using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Examine
{
	public sealed class EmptySearchResults : ISearchResults
	{
        private EmptySearchResults()
        {   
        }

	    public static ISearchResults Instance { get; } = new EmptySearchResults();

        public IEnumerator<ISearchResult> GetEnumerator() => Enumerable.Empty<ISearchResult>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Enumerable.Empty<ISearchResult>().GetEnumerator();

        public long TotalItemCount => 0;

        public IEnumerable<ISearchResult> Skip(int skip) => Enumerable.Empty<ISearchResult>();

        public IEnumerable<ISearchResult> SkipTake(int skip, int? take = null) => Enumerable.Empty<ISearchResult>();
    }
}