using Lucene.Net.Documents;
using Lucene.Net.Facet.SortedSet;
using Microsoft.Extensions.Logging;

namespace Examine.Lucene.Indexing
{
    /// <summary>
    /// Represents a facet version of <see cref="SingleType"/>
    /// </summary>
    public class FacetSingleType : SingleType
    {
        /// <inheritdoc/>
        public FacetSingleType(string fieldName, ILoggerFactory logger, bool store = true) : base(fieldName, logger, store)
        {
        }

        /// <inheritdoc/>
        protected override void AddSingleValue(Document doc, object value)
        {
            base.AddSingleValue(doc, value);

            if (!TryConvert(value, out float parsedVal))
                return;

            doc.Add(new SortedSetDocValuesFacetField(FieldName, parsedVal.ToString()));
            doc.Add(new SingleDocValuesField(FieldName, parsedVal));
        }
    }
}
