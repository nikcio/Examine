using Lucene.Net.Documents;
using Lucene.Net.Facet.SortedSet;
using Microsoft.Extensions.Logging;

namespace Examine.Lucene.Indexing
{
    /// <summary>
    /// Represents a faceted version of <see cref="DoubleType"/>
    /// </summary>
    public class FacetDoubleType : DoubleType
    {
        /// <inheritdoc/>
        public FacetDoubleType(string fieldName, ILoggerFactory logger, bool store = true) : base(fieldName, logger, store)
        {
        }

        /// <inheritdoc/>
        protected override void AddSingleValue(Document doc, object value)
        {
            base.AddSingleValue(doc, value);

            if (!TryConvert(value, out double parsedVal))
                return;

            doc.Add(new SortedSetDocValuesFacetField(FieldName, parsedVal.ToString()));
            doc.Add(new DoubleDocValuesField(FieldName, parsedVal));
        }
    }
}
