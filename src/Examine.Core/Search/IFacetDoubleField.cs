using System;
using System.Collections.Generic;
using System.Text;
using Examine.Lucene.Search;
using Lucene.Net.Facet.Range;

namespace Examine.Search
{
    /// <summary>
    /// Represents a double facet field
    /// </summary>
    public interface IFacetDoubleField : IFacetField
    {
        /// <summary>
        /// The double ranges for the field
        /// </summary>
        DoubleRange[] DoubleRanges { get; }

        /// <summary>
        /// Is the source value indexed as <see cref="float"/>
        /// </summary>
        bool IsFloat { get; set; }
    }
}
