using Newtonsoft.Json;
using System;

namespace FeedbackAPI.Models
{
    public class Feedback
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public FeedbackType Type { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "do_not_contact")]
        public bool DoNotContact { get; set; }

        [JsonProperty(PropertyName = "status")]
        public FeedbackStatus Status { get; set; }

        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "assigned_to")]
        public string AssignedTo { get; set; }

        [JsonProperty(PropertyName = "product")]
        public string Product { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
