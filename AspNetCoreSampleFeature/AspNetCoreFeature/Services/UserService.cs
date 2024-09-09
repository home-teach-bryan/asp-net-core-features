using AspNetCoreSample.Models;

namespace AspNetCoreFeature.Services;

public class UserService : IUserService
{
    private List<User> _users = new List<User>
    {
        new User { Id = Guid.NewGuid(), Name = "user1", Password = "123456", Roles = new[] { "Admin", "User" } },
        new User { Id = Guid.NewGuid(), Name = "user2", Password = "456789", Roles = new[] { "User" } },
        new User { Id = Guid.NewGuid(), Name = "user3", Password = "aa123456", Roles = new[] { "User" } },
        new User { Id = Guid.NewGuid(), Name = "user4", Password = "aabbcc", Roles = new[] { "User" } },
    };

    public (bool isValid, User user) IsValid(string name, string password)
    {
        var user = _users.FirstOrDefault(item => item.Name == name && item.Password == password);
        if (user == null)
        {
            return (false, new User());
        }
        return (true, user);
    } 

}