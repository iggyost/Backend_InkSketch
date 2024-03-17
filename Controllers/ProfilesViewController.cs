using Backend_InkSketch.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_InkSketch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesViewController : Controller
    {
        public static InkSketchDbContext context = new InkSketchDbContext();
        [HttpGet]
        [Route("get/{userId}")]
        public ActionResult<IEnumerable<ProfileView>> Get(int userId)
        {
            try
            {
                var data = context.ProfileViews.Where(x => x.UserId == userId).ToList();
                return data;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
