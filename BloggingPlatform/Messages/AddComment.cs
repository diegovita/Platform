using BloggingPlatform.Models;
using MassTransit.Mediator;
using System.Text.Json.Serialization;

namespace BloggingPlatform.Messages;

public record AddComment : Request<Comment>
{
    [JsonIgnore]
    public int BlogPostId { get; set; }
    public string Content { get; set; }
}
