using System;

namespace Examine.Facets.Search
{
    public class FacetQueryField : IFacetQueryField
    {
        private readonly IFacetQuery _query;
        private readonly SortedTextFacetField _field;

        public FacetQueryField(IFacetQuery query, SortedTextFacetField field)
        {
            _query = query;
            _field = field;
        }

        ///<inheritdoc/>
        public IFacetQueryField MaxCount(int count)
        {
            _field.MaxCount = count;

            return this;
        }

        public IFacetQuery And() => _query;
    }
}
