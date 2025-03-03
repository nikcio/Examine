using System;
using System.Collections.Generic;
using Examine.Search;
using Lucene.Net.Search;

namespace Examine.Lucene.Search
{
    /// <summary>
    /// Represents the base for a <see cref="LuceneBooleanOperation"/>
    /// </summary>
    public abstract class LuceneBooleanOperationBase : IBooleanOperation, INestedBooleanOperation, IOrdering, IFaceting
    {
        private readonly LuceneSearchQueryBase _search;

        /// <inheritdoc/>
        protected LuceneBooleanOperationBase(LuceneSearchQueryBase search)
        {
            _search = search;
        }

        /// <inheritdoc/>
        public abstract IQuery And();

        /// <inheritdoc/>
        public abstract IQuery Or();

        /// <inheritdoc/>
        public abstract IQuery Not();

        /// <summary>
        /// Allows for adding more operations to a query using the and operator
        /// </summary>
        /// <param name="inner"></param>
        /// <param name="defaultOp"></param>
        /// <returns></returns>
        public IBooleanOperation And(Func<INestedQuery, INestedBooleanOperation> inner, BooleanOperation defaultOp = BooleanOperation.And) 
            => Op(inner, BooleanOperation.And, defaultOp);

        /// <summary>
        /// Allows for adding more operations to a query using the or operator
        /// </summary>
        /// <param name="inner"></param>
        /// <param name="defaultOp"></param>
        /// <returns></returns>
        public IBooleanOperation Or(Func<INestedQuery, INestedBooleanOperation> inner, BooleanOperation defaultOp = BooleanOperation.And) 
            => Op(inner, BooleanOperation.Or, defaultOp);

        /// <summary>
        /// Allows for adding more operations to a query using the and not operator
        /// </summary>
        /// <param name="inner"></param>
        /// <param name="defaultOp"></param>
        /// <returns></returns>
        public IBooleanOperation AndNot(Func<INestedQuery, INestedBooleanOperation> inner, BooleanOperation defaultOp = BooleanOperation.And) 
            => Op(inner, BooleanOperation.Not, defaultOp);

        /// <summary>
        /// Allows for adding more operations to a query using a nested and
        /// </summary>
        /// <returns></returns>
        protected abstract INestedQuery AndNested();

        /// <summary>
        /// Allows for adding more operations to a query using a nested or
        /// </summary>
        /// <returns></returns>
        protected abstract INestedQuery OrNested();

        /// <summary>
        /// Allows for adding more operations to a query iusing a nested not
        /// </summary>
        /// <returns></returns>
        protected abstract INestedQuery NotNested();

        INestedQuery INestedBooleanOperation.And() => AndNested();
        INestedQuery INestedBooleanOperation.Or() => OrNested();
        INestedQuery INestedBooleanOperation.Not() => NotNested();

        INestedBooleanOperation INestedBooleanOperation.And(Func<INestedQuery, INestedBooleanOperation> inner, BooleanOperation defaultOp) 
            => Op(inner, BooleanOperation.And, defaultOp);

        INestedBooleanOperation INestedBooleanOperation.Or(Func<INestedQuery, INestedBooleanOperation> inner, BooleanOperation defaultOp)
            => Op(inner, BooleanOperation.Or, defaultOp);

        INestedBooleanOperation INestedBooleanOperation.AndNot(Func<INestedQuery, INestedBooleanOperation> inner, BooleanOperation defaultOp)
            => Op(inner, BooleanOperation.Not, defaultOp);

        /// <summary>
        /// Used to add a operation
        /// </summary>
        /// <param name="inner"></param>
        /// <param name="outerOp"></param>
        /// <param name="defaultInnerOp"></param>
        /// <returns></returns>
        protected internal LuceneBooleanOperationBase Op(
            Func<INestedQuery, INestedBooleanOperation> inner,
            BooleanOperation outerOp,
            BooleanOperation? defaultInnerOp = null)
        {
            _search.Queries.Push(new BooleanQuery());

            //change the default inner op if specified
            var currentOp = _search.BooleanOperation;
            if (defaultInnerOp != null)
            {
                _search.BooleanOperation = defaultInnerOp.Value;
            }

            //run the inner search
            inner(_search);

            //reset to original op if specified
            if (defaultInnerOp != null)
            {
                _search.BooleanOperation = currentOp;
            }

            return _search.LuceneQuery(_search.Queries.Pop(), outerOp);
        }

        /// <inheritdoc/>
        public abstract ISearchResults Execute(QueryOptions options = null);

        /// <inheritdoc/>
        public abstract IOrdering OrderBy(params SortableField[] fields);

        /// <inheritdoc/>
        public abstract IOrdering OrderByDescending(params SortableField[] fields);

        /// <inheritdoc/>
        public abstract IOrdering SelectFields(ISet<string> fieldNames);

        /// <inheritdoc/>
        public abstract IOrdering SelectField(string fieldName);

        /// <inheritdoc/>
        public abstract IOrdering SelectAllFields();

        /// <inheritdoc/>
        public abstract IQueryExecutor WithFacets(Action<IFacetOperations> facets);
    }
}
