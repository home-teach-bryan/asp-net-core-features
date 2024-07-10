using AspNetCoreSample.Models;

namespace AspNetCoreFeature.Services;

public interface IUserService
{
    (bool isValid, User user) IsValid(string name, string password);
}