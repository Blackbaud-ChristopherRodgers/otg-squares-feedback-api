using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using FeedbackAPI.Models;

namespace FeedbackAPI.Controllers
{
    [Route("api/note/")]
    public class NoteController : Controller
    {
        private const string EndpointUri = "https://otg-squares-docdb.documents.azure.com:443/";
        private const string PrimaryKey = "6uWD8PDP5wfXNXcQ9lB2CGKgfqTuTwKZMCuF1eUt19xUO6FJwoCSFvzDFpIoS2wYIfr8gF5Sn7Qdz35vk9n4IA==";
        private DocumentClient client;
        private DocumentDBRepository<Note> repository;

        public NoteController()
        {
            this.client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
            repository = new DocumentDBRepository<Models.Note>(this.client, "note", "note");
        }

        [HttpPost]
        public async Task<Document> AddNote([FromBody]Note value)
        {
            value.Id = Guid.NewGuid().ToString();
            value.Date = DateTime.Now;
            return await repository.CreateItemAsync(value);
        }

        [HttpGet]
        public async Task<NoteCollection> GetItems(string productId, string id)
        {
            return new NoteCollection()
            {
                Records = await repository.GetItemsAsync(productId)
            };
        }

        [HttpGet("feedbacks/{feedbackId}/{id}")]
        public async Task<Note> Get(string feedbackId, string id)
        {
            return await repository.GetItemAsync(feedbackId, id);
        }

        [HttpDelete("feedbacks/{feedbackId}/{id}")]
        public async Task Delete(string feedbackId, string id)
        {
            await repository.DeleteItemAsync(feedbackId, id);
        }
    }
}
