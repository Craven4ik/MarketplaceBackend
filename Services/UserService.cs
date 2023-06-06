using MarketPlace.Models;
using MarketplaceBackend.Data;
using MarketplaceBackend.Services.IServices;

namespace MarketplaceBackend.Services;

public class UserService : IUserService
{
    private readonly UserDbContext _userDbContext;

    public UserService(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public string FindUserIdByEmail(string Email)
        => _userDbContext.Users.FirstOrDefault(p => p.Email == Email).Id;

    public User FindUserByEmail(string Email)
    {
        var user = _userDbContext.Users.FirstOrDefault(p => p.Email == Email);
        return new User
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
        };
    }

    public User FindUserById(string Id)
    {
        var user = _userDbContext.Users.FirstOrDefault(u => u.Id == Id);
        return new User
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };
    }
}
