namespace AspNetCoreSample.Models;

public class User
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Password { get; set; }
    
    public string[] Roles { get; set; }
}