using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
     
    [Route("Categories")]
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
