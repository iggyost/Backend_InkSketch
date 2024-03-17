using Backend_InkSketch.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_InkSketch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : Controller
    {
        public static InkSketchDbContext context = new InkSketchDbContext();

        [HttpGet]
        [Route("get")]
        public ActionResult<IEnumerable<Tag>> Get()
        {
            try
            {
                return context.Tags.ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
