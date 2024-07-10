using System.ComponentModel.DataAnnotations;

namespace AspNetCoreFeature.Models.Request;

public class GetTokenRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Password { get; set; }
}