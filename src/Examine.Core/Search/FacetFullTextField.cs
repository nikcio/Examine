namespace Examine.Search
{
    /// <inheritdoc/>
    public class FacetFullTextField : IFacetFullTextField
    {
        /// <inheritdoc/>
        public int MaxCount { get; set; }

        /// <inheritdoc/>
        public string[] Values { get; set; }

        /// <inheritdoc/>
        public string Field { get; set; }

        /// <inheritdoc/>
        public string FacetField { get; set; }

        /// <inheritdoc/>
        public FacetFullTextField(string field, string[] values, int maxCount = 10, string facetField = ExamineFieldNames.DefaultFacetsName)
        {
            Field = field;
            Values = values;
            MaxCount = maxCount;
            FacetField = facetField;
        }
    }
}
