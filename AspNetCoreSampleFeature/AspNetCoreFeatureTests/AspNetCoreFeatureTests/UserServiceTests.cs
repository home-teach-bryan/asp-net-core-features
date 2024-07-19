using AspNetCoreFeature.Services;
using FluentAssertions;

namespace AspNetCoreFeatureTests;

public class UserServiceTests
{
    private UserService _userService;
    
    [SetUp]
    public void Setup()
    {
        _userService = new UserService();
    }

    [Test]
    public void UserService_IsValid_ReturnFalse()
    {
        // arrange
        var user = "name";
        var password = "password";
        
        // actual
        var actual = _userService.IsValid(user, password);

        // assert
        actual.isValid.Should().BeFalse();
    }
    
    
    [Test]
    public void UserService_IsValid_ReturnTrue()
    {
        // arrange
        var user = "user1";
        var password = "123456";
        
        // actual
        var actual = _userService.IsValid(user, password);

        // assert
        actual.isValid.Should().BeTrue();
    }
    
}