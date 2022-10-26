using System.Diagnostics;
using Examine.Lucene.Providers;
using Examine.Search;
using Examine.Web.Demo.Controllers;
using Examine.Web.Demo.Data.Models;
using Lucene.Net.Search;

namespace Examine.Web.Demo.Data
{
    public class IndexService
    {
        private readonly IExamineManager _examineManager;
        private readonly BogusDataService _bogusDataService;

        public IndexService(IExamineManager examineManager, BogusDataService bogusDataService) {
            _examineManager = examineManager;
            _bogusDataService = bogusDataService;
        }

        public void RebuildIndex(string indexName, int dataSize)
        {
            IIndex index = GetIndex(indexName);

            index.CreateIndex();

            IEnumerable<ValueSet> data = _bogusDataService.GenerateData(dataSize);

            index.IndexItems(data);
        }

        public IndexInformation GetIndexInformation(string indexName)
        {
            IIndex index = GetIndex(indexName);

            if (index is IIndexStats indexStats)
            {
                IEnumerable<string> fields = indexStats.GetFieldNames();
                return new IndexInformation(
                    indexStats.GetDocumentCount(),
                    fields.ToList());
            }
            else
            {
                throw new InvalidOperationException($"Failed to get index stats on {indexName}");
            }
        }

        public void AddToIndex(string indexName, int dataSize)
        {
            IIndex index = GetIndex(indexName);

            IEnumerable<ValueSet> data = _bogusDataService.GenerateData(dataSize);

            index.IndexItems(data);
        }

        public IEnumerable<IIndex> GetAllIndexes() => _examineManager.Indexes;

        public ISearchResults SearchNativeQuery(string indexName, string query)
        {
            IIndex index = GetIndex(indexName);

            ISearcher searcher = index.Searcher;
            IQuery criteria = searcher.CreateQuery();
            return criteria.NativeQuery(query).Execute();
        }

        public ISearchResults SearchNativeQueryAcrossIndexes(string query)
        {
            if (!_examineManager.TryGetSearcher("MultiIndexSearcher", out ISearcher? multiIndexSearcher))
            {
                throw new InvalidOperationException("Failed to get MultiIndexSearcher");
            }

            ISearcher searcher = multiIndexSearcher;
            IQuery criteria = searcher.CreateQuery();
            return criteria.NativeQuery(query).Execute();
        }

        public ISearchResults GetAllIndexedItems(string indexName, int skip, int take)
        {
            IIndex index = GetIndex(indexName);

            ISearcher searcher = index.Searcher;
            IQuery criteria = searcher.CreateQuery();
            return criteria.All().Execute(QueryOptions.SkipTake(skip, take));
        }

        private IIndex GetIndex(string indexName)
        {
            if (!_examineManager.TryGetIndex(indexName, out IIndex? index))
            {
                throw new ArgumentException($"Index '{indexName}' not found");
            }

            return index;
        }
    }

}
