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
}
