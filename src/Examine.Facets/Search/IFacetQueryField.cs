using System;

namespace Examine.Facets.Search
{
    public interface IFacetQueryField
    {
        /// <summary>
        /// Maximum number of terms to return
        /// </summary>
        IFacetQueryField MaxCount(int count);

        IFacetQuery And();
    }
}
