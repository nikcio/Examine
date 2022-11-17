using Examine.Lucene.Providers;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Microsoft.Extensions.Logging;

namespace Examine.Lucene.Indexing
{
    /// <summary>
    /// Represents a Double <see cref="IndexFieldRangeValueType{T}"/>
    /// </summary>
    public class DoubleType : IndexFieldRangeValueType<double>
    {
        /// <inheritdoc/>
        public DoubleType(string fieldName, ILoggerFactory logger, bool store= true)
            : base(fieldName, logger, store)
        {
        }

        /// <summary>
        /// Can be sorted by the normal field name
        /// </summary>
        public override string SortableFieldName => FieldName;

        /// <inheritdoc/>
        protected override void AddSingleValue(Document doc, object value)
        {
            if (!TryConvert(value, out double parsedVal))
                return;

            doc.Add(new DoubleField(FieldName,parsedVal, Store ? Field.Store.YES : Field.Store.NO));
        }

        /// <inheritdoc/>
        public override Query GetQuery(string query)
        {
            return !TryConvert(query, out double parsedVal) ? null : GetQuery(parsedVal, parsedVal);
        }

        /// <inheritdoc/>
        public override Query GetQuery(double? lower, double? upper, bool lowerInclusive = true, bool upperInclusive = true)
        {
            return NumericRangeQuery.NewDoubleRange(FieldName,
                lower ?? double.MinValue,
                upper ?? double.MaxValue, lowerInclusive, upperInclusive);
        }
    }
}
