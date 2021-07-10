using Microsoft.AspNetCore.Mvc;

namespace EbayAlike.Web.Controllers
{
    [Route("{controller}")]
    [ApiController]
    public class ProductController : Controller
    {
        [Route("all")]
        public IActionResult GetAllPosts()
        {
            return View();
        }
    }
}
