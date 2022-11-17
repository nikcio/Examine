using Lucene.Net.Documents;
using Lucene.Net.Facet.SortedSet;
using Microsoft.Extensions.Logging;

namespace Examine.Lucene.Indexing
{
    /// <summary>
    /// Represents a facet version of <see cref="Int32Type"/>
    /// </summary>
    public class FacetInt32Type : Int32Type
    {
        /// <inheritdoc/>
        public FacetInt32Type(string fieldName, ILoggerFactory logger, bool store = true) : base(fieldName, logger, store)
        {
        }

        /// <inheritdoc/>
        protected override void AddSingleValue(Document doc, object value)
        {
            base.AddSingleValue(doc, value);

            if (!TryConvert(value, out int parsedVal))
                return;

            doc.Add(new SortedSetDocValuesFacetField(FieldName, parsedVal.ToString()));
            doc.Add(new NumericDocValuesField(FieldName, parsedVal));
        }
    }
}
