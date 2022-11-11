using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Examine.Facets
{
    public class FacetResult : IFacetResult
    {
        private readonly IEnumerable<IFacetValue> _values;

        public FacetResult(IEnumerable<IFacetValue> values)
        {
            _values = values;
        }

        public IEnumerator<IFacetValue> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
