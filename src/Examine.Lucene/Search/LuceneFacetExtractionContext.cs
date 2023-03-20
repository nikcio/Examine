using System;
using Lucene.Net.Facet;
using Lucene.Net.Facet.SortedSet;
using Lucene.Net.Facet.Taxonomy;

namespace Examine.Lucene.Search
{
    public class LuceneFacetExtractionContext : IFacetExtractionContext
    {

        private SortedSetDocValuesReaderState _sortedSetReaderState = null;

        public LuceneFacetExtractionContext(FacetsCollector facetsCollector, ISearcherReference searcherReference, FacetsConfig facetConfig)
        {
            FacetsCollector = facetsCollector;
            FacetConfig = facetConfig;
            SearcherReference = searcherReference;
        }

        public FacetsCollector FacetsCollector { get; }

        public FacetsConfig FacetConfig { get; }

        public ISearcherReference SearcherReference { get; }

        public virtual Facets GetFacetCounts(string facetIndexFieldName, bool isTaxonomyIndexed)
        {
            if (isTaxonomyIndexed)
            {
                if (SearcherReference is ITaxonomySearcherReference taxonomySearcher)
                {
                    return new FastTaxonomyFacetCounts(facetIndexFieldName, taxonomySearcher.TaxonomyReader, FacetConfig, FacetsCollector);
                }
                throw new InvalidOperationException("Cannot get FastTaxonomyFacetCounts for field not stored in the Taxonomy index");
            }
            else
            {
                if (_sortedSetReaderState == null || !_sortedSetReaderState.Field.Equals(facetIndexFieldName))
                {
                    _sortedSetReaderState = new DefaultSortedSetDocValuesReaderState(SearcherReference.IndexSearcher.IndexReader, facetIndexFieldName);
                }
                return new SortedSetDocValuesFacetCounts(_sortedSetReaderState, FacetsCollector);
            }
        }
    }
}
