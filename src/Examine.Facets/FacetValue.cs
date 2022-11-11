using System;

namespace Examine.Facets
{
    public class FacetValue : IFacetValue
    {
        public FacetValue(string label, float value)
        {
            Label = label;
            Value = value;
        }

        public string Label { get; }

        public float Value { get; }
    }
}
