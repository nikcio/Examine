using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Examine.Lucene.Search;

namespace Examine.Search
{
    /// <inheritdoc/>
    public class FacetResult : IFacetResult
    {
        private readonly IEnumerable<IFacetValue> _values;

        /// <inheritdoc/>
        public FacetResult(IEnumerable<IFacetValue> values)
        {
            _values = values;
        }

        /// <inheritdoc/>
        public IEnumerator<IFacetValue> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        /// <inheritdoc/>
        public IFacetValue? Facet(string label)
        {
            return _values.FirstOrDefault(field => field.Label.Equals(label));
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
