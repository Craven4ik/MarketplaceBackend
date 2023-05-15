using MarketPlace.Models;
using MarketplaceBackend.DTO;
using MarketplaceBackend.Services;
using MarketplaceBackend.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("GetItems")]
        public List<Item> GetItems()
        {
            return _itemService.GetItems();
        }

        [HttpDelete("DeleteItem")]
        public void Delete(int id)
        {
            _itemService.Delete(id);
        }

        [HttpPost("CreateItem")]
        public Item CreateItem(ItemDTO _item)
        {
            return _itemService.CreateItem(_item);
        }

        [HttpPut("UpdateItem")]
        public Item UpdateItem(ItemDTO _item)
        {
            return _itemService.UpdateItem(_item);
        }
    }
}
