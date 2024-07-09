using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BloggingPlatform.Models;

public class LoginModel
{
    [JsonIgnore]
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}
