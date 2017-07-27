using Newtonsoft.Json;

namespace FeedbackAPI.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "last_name")]

        public string LastName { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }
    }
}
