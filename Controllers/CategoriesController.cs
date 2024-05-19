using Backend_InkSketch.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_InkSketch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        public static InkSketchDbContext context = new InkSketchDbContext();
        [HttpGet]
        [Route("get")]
        public ActionResult<IEnumerable<Category>> Get()
        {
            try
            {
                var categoryList = context.Categories.ToList();
                return categoryList;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
