using MarketPlace.Models;

namespace MarketplaceBackend.Services.IServices
{
    public interface IUserService
    {
        User FindUserByEmail(string email);
        User FindUserById(string Id);
    }
}
