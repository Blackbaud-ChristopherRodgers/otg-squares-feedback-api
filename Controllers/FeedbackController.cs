using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using FeedbackAPI.Models;

namespace FeedbackAPI.Controllers
{
    [Route("api/feedback/")]
    public class FeedbackController : Controller
    {
        private const string EndpointUri = "https://otg-squares-docdb.documents.azure.com:443/";
        private const string PrimaryKey = "6uWD8PDP5wfXNXcQ9lB2CGKgfqTuTwKZMCuF1eUt19xUO6FJwoCSFvzDFpIoS2wYIfr8gF5Sn7Qdz35vk9n4IA==";
        private DocumentClient client;
        private DocumentDBRepository<Feedback> repository;

        public FeedbackController()
        {
            this.client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
            repository = new DocumentDBRepository<Models.Feedback>(this.client, "feedback", "feedback");
        }

        [HttpPost]
        public async Task<Document> AddFeedback([FromBody]Feedback value)
        {
            value.Id = Guid.NewGuid().ToString();
            value.Status = FeedbackStatus.New;
            value.Date = DateTime.Now;
            return await repository.CreateItemAsync(value);
        }

        [HttpGet]
        public async Task<FeedbackCollection> GetItems(string productId, string id)
        {
            return new FeedbackCollection()
            {
                Records = await repository.GetItemsAsync(productId, f => string.IsNullOrWhiteSpace(f.Id))
            };
        }

        [HttpGet("products/{productId}/{id}")]
        public async Task<Feedback> Get(string productId, string id)
        {
            return await repository.GetItemAsync(productId, id);
        }

        [HttpPut("{id}")]
        public async Task<Document> Put(string id, [FromBody]Feedback value)
        {
            return await repository.UpdateItemAsync(id, value);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await repository.DeleteItemAsync(id);
        }
    }
}
