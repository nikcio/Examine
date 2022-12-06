namespace Examine.Search
{
    /// <summary>
    /// Represents a facet double range query field
    /// </summary>
    public interface IFacetDoubleRangeQueryField : IBooleanOperation
    {
        /// <summary>
        /// Sets if the range query is on <see cref="float"/> values
        /// </summary>
        /// <param name="isFloat"></param>
        /// <returns></returns>
        IFacetDoubleRangeQueryField IsFloat(bool isFloat);
    }
}
