using MarketPlace.Models;
using MarketplaceBackend.Data;
using MarketplaceBackend.DTO;
using MarketplaceBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceBackend.Services
{
    public class ItemService : IItemService
    {
        private readonly UserDbContext _userDbContext;
        private readonly IUserService _userService;

        public ItemService(UserDbContext userDbContext, IUserService userService)
        {
            _userDbContext = userDbContext;
            _userService = userService;
        }
        public List<Item> GetItems()
        {
            return _userDbContext.Items.Include(c=> c.User).ToList();
        }

        public void Delete(int id)
        {
            var item = _userDbContext.Items.FirstOrDefault(p => p.Id == id);
            _userDbContext.Items.Remove(item);
            _userDbContext.SaveChanges();
        }

        public Item CreateItem(ItemDTO _item)
        {
            var item = new Item
            {
                Name = _item.Name,
                Price = _item.Price,
                Image = _item.Image,
                Description = _item.Description,
                OwnerEmail = _item.OwnerEmail,
                User = _userService.FindUserByEmail(_item.OwnerEmail)
            };
            _userDbContext.Items.Add(item);
            _userDbContext.SaveChanges();
            //return _userDbContext.Items.Include(c=> c.User).FirstOrDefault(p => p.Id == item.Id);
            return item;
        }

        public Item UpdateItem(ItemDTO _item)
        {
            var item = new Item
            {
                Name = _item.Name,
                Price = _item.Price,
                Image = _item.Image,
                Description = _item.Description,
                OwnerEmail = _item.OwnerEmail,
                User = _userService.FindUserByEmail(_item.OwnerEmail)
            };
            _userDbContext.Items.Update(item);
            _userDbContext.SaveChanges();
            return item;
        }
    }
}
