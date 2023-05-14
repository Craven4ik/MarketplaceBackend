using MarketPlace.Models;
using MarketplaceBackend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly UserDbContext _userDbContext;
        
        public ItemController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        [HttpGet]
        public List<Item> GetItems()
        {
            return _userDbContext.Items.ToList();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var item = _userDbContext.Items.FirstOrDefault(p => p.Id == id);
            _userDbContext.Items.Remove(item);
            _userDbContext.SaveChanges();
        }

        [HttpPost]
        public Item CreateItem(Item item)
        {
            _userDbContext.Items.Add(item);
            _userDbContext.SaveChanges();
            return item;
        }

        [HttpPost]
        public Item UpdateItem(Item item)
        {
            _userDbContext.Items.Update(item);
            _userDbContext.SaveChanges();
            return item;
        }
    }
}
