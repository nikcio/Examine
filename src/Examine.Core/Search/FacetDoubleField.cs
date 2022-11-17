using System;
using System.Collections.Generic;
using System.Text;
using Examine.Lucene.Search;
using Lucene.Net.Facet.Range;

namespace Examine.Search
{
    /// <inheritdoc/>
    public class FacetDoubleField : IFacetDoubleField
    {
        /// <inheritdoc/>
        public DoubleRange[] DoubleRanges { get; }

        /// <inheritdoc/>
        public string Field { get; }

        /// <inheritdoc/>
        public string FacetField { get; set; } = "$facets";

        /// <inheritdoc/>
        public bool IsFloat { get; set; }

        /// <inheritdoc/>
        public FacetDoubleField(string field, DoubleRange[] doubleRanges)
        {
            Field = field;
            DoubleRanges = doubleRanges;
        }
    }
}
