namespace Examine.Lucene.Search
{
    /// <summary>
    /// Represents a base facet field
    /// </summary>
    public interface IFacetField
    {
        /// <summary>
        /// The field name
        /// </summary>
        string Field { get; }

        /// <summary>
        /// The field to get the facet field from
        /// </summary>
        string FacetField { get; set; }
    }
}
