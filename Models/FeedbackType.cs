using System.ComponentModel.DataAnnotations;

namespace FeedbackAPI.Models
{
    public enum FeedbackType
    {
        [Display(Name = "positive", Description = "The feedback collected when the response is positive.")]
        Positive = 0,

        [Display(Name = "negative", Description = "The feedback collected when the responsive is negative.")]
        Negative = 1
    }
}
