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
    /// <summary>
    /// Represents a Int64 <see cref="IndexFieldRangeValueType{T}"/>
    /// </summary>
    public class Int64Type : IndexFieldRangeValueType<long>, IIndexFacetValueType
    {
        private readonly bool _isFacetable;

        /// <inheritdoc/>
        public Int64Type(string fieldName, ILoggerFactory logger, bool store, bool isFacetable)
            : base(fieldName, logger, store)
        {
            _isFacetable = isFacetable;
        }

        /// <inheritdoc/>
        public Int64Type(string fieldName, ILoggerFactory logger, bool store = true)
            : base(fieldName, logger, store)
        {
            _isFacetable = false;
        }

        /// <summary>
        /// Can be sorted by the normal field name
        /// </summary>
        public override string SortableFieldName => FieldName;

        /// <inheritdoc/>
        protected override void AddSingleValue(Document doc, object value)
        {
            if (!TryConvert(value, out long parsedVal))
                return;

            doc.Add(new Int64Field(FieldName, parsedVal, Store ? Field.Store.YES : Field.Store.NO));

            if (_isFacetable)
            {
                doc.Add(new SortedSetDocValuesFacetField(FieldName, parsedVal.ToString()));
                doc.Add(new NumericDocValuesField(FieldName, parsedVal));
            }
        }

        /// <inheritdoc/>
        public override Query GetQuery(string query)
        {
            return !TryConvert(query, out long parsedVal) ? null : GetQuery(parsedVal, parsedVal);
        }

        /// <inheritdoc/>
        public override Query GetQuery(long? lower, long? upper, bool lowerInclusive = true, bool upperInclusive = true)
        {
            return NumericRangeQuery.NewInt64Range(FieldName,
                lower,
                upper, lowerInclusive, upperInclusive);
        }

        /// <inheritdoc/>
        public virtual IEnumerable<KeyValuePair<string, IFacetResult>> ExtractFacets(IFacetExtractionContext facetExtractionContext, IFacetField field)
            => field.ExtractFacets(facetExtractionContext);
    }
}
