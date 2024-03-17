using Backend_InkSketch.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_InkSketch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersTagsController : Controller
    {
        public static InkSketchDbContext context = new InkSketchDbContext();

        [HttpGet]
        [Route("set/{userId}/{tagId}")]
        public ActionResult<IEnumerable<UsersTag>> SelectTags(int userId, int tagId)
        {
            try
            {
                if (userId != 0 && tagId != 0)
                {
                    UsersTag usersTag = new UsersTag()
                    {
                        UserId = userId,
                        TagId = tagId,
                    };
                    context.UsersTags.Add(usersTag);
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
