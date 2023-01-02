using System.Collections.Generic;
using Examine.Lucene.Providers;
using Examine.Lucene.Search;
using Examine.Search;
using Lucene.Net.Documents;
using Lucene.Net.Facet;
using Lucene.Net.Facet.SortedSet;
using Lucene.Net.Search;
using Microsoft.Extensions.Logging;

namespace Examine.Lucene.Indexing
{
    public class DoubleType : IndexFieldRangeValueType<double>, IIndexFacetValueType
    {
        private readonly bool _isFacetable;

        public DoubleType(string fieldName, ILoggerFactory logger, bool store = true, bool isFacetable = false)
            : base(fieldName, logger, store)
        {
            _isFacetable = isFacetable;
        }

        /// <summary>
        /// Can be sorted by the normal field name
        /// </summary>
        public override string SortableFieldName => FieldName;

        protected override void AddSingleValue(Document doc, object value)
        {
            if (!TryConvert(value, out double parsedVal))
                return;

            doc.Add(new DoubleField(FieldName,parsedVal, Store ? Field.Store.YES : Field.Store.NO));

            if (_isFacetable)
            {
                doc.Add(new SortedSetDocValuesFacetField(FieldName, parsedVal.ToString()));
                doc.Add(new DoubleDocValuesField(FieldName, parsedVal));
            }
        }

        public override Query GetQuery(string query)
        {
            return !TryConvert(query, out double parsedVal) ? null : GetQuery(parsedVal, parsedVal);
        }

        public override Query GetQuery(double? lower, double? upper, bool lowerInclusive = true, bool upperInclusive = true)
        {
            return NumericRangeQuery.NewDoubleRange(FieldName,
                lower ?? double.MinValue,
                upper ?? double.MaxValue, lowerInclusive, upperInclusive);
        }
        public virtual void ExtractFacets(FacetsCollector facetsCollector, SortedSetDocValuesReaderState sortedSetReaderState, Dictionary<string, IFacetResult> facets, IFacetField field) => field.ExtractFacets(facetsCollector, sortedSetReaderState, facets);
    }
}
