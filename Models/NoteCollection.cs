using Newtonsoft.Json;
using System.Collections.Generic;

namespace FeedbackAPI.Models
{
    public class NoteCollection
    {
        [JsonProperty(PropertyName = "records")]
        public IEnumerable<Note> Records { get; set; }
    }
}
