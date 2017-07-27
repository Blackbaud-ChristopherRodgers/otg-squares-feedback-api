using System.Collections.Generic;

namespace FeedbackAPI.Models
{
    public class FeedbackCollection
    {
        public IEnumerable<Feedback> Records { get; set; }
    }
}
