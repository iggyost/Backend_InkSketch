using Backend_InkSketch.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_InkSketch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsViewController : Controller
    {
        public static InkSketchDbContext context = new InkSketchDbContext();
        [HttpGet]
        [Route("get")]
        public ActionResult<IEnumerable<TagsView>> Get()
        {
            try
            {
                var data = context.TagsViews.ToList();
                return data.ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
