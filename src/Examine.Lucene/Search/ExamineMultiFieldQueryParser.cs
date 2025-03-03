using Examine.Lucene.Indexing;
using Lucene.Net.Analysis;
using Lucene.Net.Search;
using Lucene.Net.Util;

namespace Examine.Lucene.Search
{
    /// <summary>
    /// Custom query parser to deal with Examine/Lucene field value types
    /// </summary>
    public class ExamineMultiFieldQueryParser : CustomMultiFieldQueryParser
    {
        private readonly ISearchContext _searchContext;

        /// <inheritdoc/>
        public ExamineMultiFieldQueryParser(ISearchContext searchContext, LuceneVersion matchVersion, Analyzer analyzer)
            : base(matchVersion, searchContext.SearchableFields, analyzer)
        {
            _searchContext = searchContext ?? throw new System.ArgumentNullException(nameof(searchContext));
        }

        /// <summary>
        /// Override to provide support for numerical range query parsing
        /// </summary>
        /// <param name="field"></param>
        /// <param name="part1"></param>
        /// <param name="part2"></param>
        /// <param name="startInclusive"></param>
        /// <param name="endInclusive"></param>
        /// <returns></returns>
        /// <remarks>
        /// By Default the lucene query parser only deals with strings and the result is a TermRangeQuery, however for numerics it needs to be a
        /// NumericRangeQuery. We can override this method to provide that behavior.
        /// 
        /// In previous releases people were complaining that this wouldn't work and this is why. The answer came from here https://stackoverflow.com/questions/5026185/how-do-i-make-the-queryparser-in-lucene-handle-numeric-ranges
        /// </remarks>
        ///    protected override Query GetRangeQuery(string field, string part1, string part2, bool startInclusive,bool endInclusive)
        protected override Query GetRangeQuery(string field, string part1, string part2, bool startInclusive,bool endInclusive)
        {
            // if the field is IIndexRangeValueType then return it's query, else return the default
            var fieldType = _searchContext.GetFieldValueType(field);
            if (fieldType != null && fieldType is IIndexRangeValueType rangeType)
            {
                return rangeType.GetQuery(part1, part2, startInclusive, endInclusive);
            }

            return base.GetRangeQuery(field, part1, part2, startInclusive, endInclusive);
        }
    }
}
