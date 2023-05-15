using MarketPlace.Models;
using MarketplaceBackend.DTO;

namespace MarketplaceBackend.Services.IServices
{
    public interface IItemService
    {
        List<Item> GetItems();
        void Delete(int id);
        Item CreateItem(ItemDTO _item);
        Item UpdateItem(ItemDTO _item);
        Item FindItemById(int Id);
    }
}
