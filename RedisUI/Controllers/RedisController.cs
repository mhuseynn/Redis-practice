using Microsoft.AspNetCore.Mvc;
using RedisUI.Models;
using StackExchange.Redis;

namespace RedisUI.Controllers
{
    public class RedisController : Controller
    {
        public ActionResult Index()
        {

            string listKey = "myList";


            var items = new List<string>();
            foreach (var item in RedisConfig.Database.ListRange(listKey, 0, -1))
            {
                items.Add(item.ToString());
            }


            return View(items);
        }
    }
}
