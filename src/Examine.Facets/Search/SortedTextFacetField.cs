using System;

namespace Examine.Facets.Search
{
    public class SortedTextFacetField : FacetField, ISortedTextFacetField
    {
        public SortedTextFacetField(string name) : base(name)
        {
        }

        public SortedTextFacetField(string name, string[] values) : base(name)
        {
            Values = values;
        }

        public string[] Values { get; internal set; }

        public int MaxCount { get; internal set; } = 10;

        public override FacetType Type { get; } = FacetType.SortedText;
    }
}
