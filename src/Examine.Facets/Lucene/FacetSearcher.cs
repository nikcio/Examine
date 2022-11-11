using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Examine.Lucene;
using Examine.Lucene.Providers;
using Examine.Lucene.Search;
using Examine.Search;
using Lucene.Net.Analysis;
using Lucene.Net.Search;

namespace Examine.Facets.Lucene
{
    public class FacetSearcher : LuceneSearcher
    {
        public FacetSearcher(string name, SearcherManager searcherManager, Analyzer analyzer, FieldValueTypeCollection fieldValueTypeCollection) : base(name, searcherManager, analyzer, fieldValueTypeCollection)
        {
        }

        public override IQuery CreateQuery(string category = null, BooleanOperation defaultOperation = BooleanOperation.And)
        {
            return new FacetSearchQuery(GetSearchContext(), category, LuceneAnalyzer, new LuceneSearchOptions(), defaultOperation);
        }
    }
}
