using MarketPlace.Models;

namespace MarketplaceBackend.Services.IServices
{
    public interface IUserService
    {
        string FindUserIdByEmail(string email);
        User FindUserByEmail(string email);
        User FindUserById(string Id);
    }
}
