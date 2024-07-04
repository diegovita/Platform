using BloggingPlatform.Models;
using MassTransit.Mediator;
using System.ComponentModel.DataAnnotations;

namespace BloggingPlatform.Messages
{
    public class GetToken : Request<BearerToken>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
