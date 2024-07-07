using System.ComponentModel.DataAnnotations;

namespace BloggingPlatform.Dto;

public record LoginDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}
