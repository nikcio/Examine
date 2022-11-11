using System;
using System.Collections.Generic;
using System.Text;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Facet.SortedSet;
using Lucene.Net.Search;
using Microsoft.Extensions.Logging;

namespace Examine.Lucene.Indexing
{
    public class TextFacetType : FullTextType
    {
        public TextFacetType(string fieldName, ILoggerFactory logger, Analyzer analyzer, bool sortable = false) : base(fieldName, logger, analyzer, sortable)
        {
        }

        protected override void AddSingleValue(Document doc, object value)
        {
            base.AddSingleValue(doc, value);

            if (TryConvert<string>(value, out var str))
            {
                doc.Add(new SortedSetDocValuesFacetField(FieldName, str));
            }
        }
    }
}
