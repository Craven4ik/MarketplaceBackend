using MarketPlace.Models;
using MarketplaceBackend.Data;
using MarketplaceBackend.DTO;
using MarketplaceBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceBackend.Services;

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
        return _userDbContext.Items.ToList();
    }

    public void Delete(int id)
    {
        var item = _userDbContext.Items.FirstOrDefault(p => p.Id == id);
        _userDbContext.Items.Remove(item);
        _userDbContext.SaveChanges();
    }

    public Item CreateItem(ItemDTO _item)
    {
        //var newUser = _userService.FindUserByEmail(_item.OwnerEmail);
        _userDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        var item = Map(_item);
        _userDbContext.Items.Add(item);
        _userDbContext.Entry(item.User).State = EntityState.Unchanged;
        _userDbContext.SaveChanges();
        return item;
    }

    public Item UpdateItem(ItemDTO dto)
    {
        _userDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        var item = Map(dto); 
        _userDbContext.Items.Update(item);
        _userDbContext.Entry(item.User).State = EntityState.Unchanged;
        _userDbContext.SaveChanges();
        return item;
    }

    public Item Map(ItemDTO dto)
        => new Item
        {
            Id = dto.Id,
            Name = dto.Name,
            Price = dto.Price,
            Image = dto.Image,
            Description = dto.Description,
            OwnerEmail = dto.OwnerEmail,
            User = _userService.FindUserByEmail(dto.OwnerEmail),
        };

    public Item FindItemById(int Id)
    {
        var item = _userDbContext.Items.Include(c => c.User).FirstOrDefault(u => u.Id == Id);
        return new Item
        {
            Id = item.Id,
            Name = item.Name,
            Price = item.Price,
            Image = item.Image,
            Description = item.Description,
            OwnerEmail = item.OwnerEmail,
            User = item.User,
        };
    }
}
