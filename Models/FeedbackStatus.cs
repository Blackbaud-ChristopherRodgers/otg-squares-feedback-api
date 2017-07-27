using System;
using System.ComponentModel.DataAnnotations;

namespace FeedbackAPI.Models
{
    public enum FeedbackStatus
    {
        [Display(Name = "new", Description = "For new feedback.")]
        New = 0,

        [Display(Name = "awaiting_client", Description = "Waiting for client feedback to procede.")]
        AwaitingClient = 1,

        [Display(Name = "closed", Description = "Feedback that has been acted on and there is no work left.")]
        Closed = 2
    }
}
