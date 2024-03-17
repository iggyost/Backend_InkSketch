using Backend_InkSketch.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_InkSketch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesViewController : Controller
    {
        public static InkSketchDbContext context = new InkSketchDbContext();
        [HttpGet]
        [Route("get/{userId}")]
        public ActionResult<IEnumerable<FavoritesView>> Get(int userId)
        {
            try
            {
                var favoritesImages = context.FavoritesViews.Where(x => x.UserId == userId).ToList();
                return favoritesImages;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
