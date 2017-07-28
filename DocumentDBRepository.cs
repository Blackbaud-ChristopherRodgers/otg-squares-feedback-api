namespace FeedbackAPI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Azure.Documents.Linq;

    public class DocumentDBRepository<T> where T : class
    {
        private string DatabaseId;
        private string CollectionId;
        private DocumentClient client;

        internal DocumentDBRepository(DocumentClient client, string databaseId, string collectionId) 
        {
            this.client = client;
            this.DatabaseId = databaseId;
            this.CollectionId = collectionId;
        }

        public async Task<T> GetItemAsync(string partitionKey, string id)
        {
            try
            {
                Document document = await this.client.ReadDocumentAsync(UriFactory.CreateDocumentUri(this.DatabaseId, this.CollectionId, id), new RequestOptions()
                {
                    PartitionKey = new PartitionKey(partitionKey)
                });
                return (T)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync(string partitionKey)
        {
            IDocumentQuery<T> query = this.client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(this.DatabaseId, this.CollectionId),
                new FeedOptions { MaxItemCount = -1 })
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public async Task<Document> CreateItemAsync(T item)
        {
            return await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(this.DatabaseId, this.CollectionId), item);
        }

        public async Task<Document> UpdateItemAsync(string partitionKey, string id, T item)
        {
            return await this.client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(this.DatabaseId, this.CollectionId, id), item, new RequestOptions()
            {
                PartitionKey = new PartitionKey(partitionKey)
            });
        }

        public async Task DeleteItemAsync(string partitionKey, string id)
        {
            await this.client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(this.DatabaseId, this.CollectionId, id), new RequestOptions()
            {
                PartitionKey = new PartitionKey(partitionKey)
            });
        }
    }
}