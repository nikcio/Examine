namespace Examine.Search
{
    public interface IFacetDoubleRangeQueryField : IFacetAppending, IQueryExecutor
    {
        /// <summary>
        /// Sets the field where the facet information will be read from
        /// </summary>
        IFacetDoubleRangeQueryField FacetField(string fieldName);
    }
}
