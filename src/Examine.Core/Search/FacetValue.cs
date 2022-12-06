using System;
using System.Collections.Generic;
using System.Text;
using Examine.Lucene.Search;

namespace Examine.Search
{
    /// <inheritdoc/>
    public class FacetValue : IFacetValue
    {
        /// <inheritdoc/>
        public string Label { get; }

        /// <inheritdoc/>
        public float Value { get; }

        /// <inheritdoc/>
        public FacetValue(string label, float value)
        {
            Label = label;
            Value = value;
        }
    }
}
