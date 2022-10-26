using Examine.Lucene.Providers;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Microsoft.Extensions.Logging;

namespace Examine.Lucene.Indexing
{
    public class DoubleType : IndexFieldRangeValueType<double>
    {
        public DoubleType(string fieldName, ILoggerFactory logger, bool store = true)
            : base(fieldName, logger, store)
        {
        }

        /// <summary>
        /// Can be sorted by the normal field name
        /// </summary>
        public override string SortableFieldName => FieldName;

        protected override void AddSingleValue(Document doc, object value)
        {
            if (!TryConvert(value, out double parsedVal))
                return;

            doc.Add(new DoubleField(FieldName, parsedVal, Store ? Field.Store.YES : Field.Store.NO));
        }

        public override Query GetQuery(string query) => !TryConvert(query, out double parsedVal) ? null : GetQuery(parsedVal, parsedVal);

        public override Query GetQuery(double? lower, double? upper, bool lowerInclusive = true, bool upperInclusive = true) => NumericRangeQuery.NewDoubleRange(FieldName,
                lower ?? double.MinValue,
                upper ?? double.MaxValue, lowerInclusive, upperInclusive);
    }
}
