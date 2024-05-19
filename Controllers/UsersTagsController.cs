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
        [Route("set/{userId}/{firstTag}/{secondTag}")]
        public ActionResult<IEnumerable<UsersTag>> SetTags(int userId, int firstTag, int secondTag)
        {
            try
            {
                UsersTag firstUserTag = new UsersTag()
                {
                    UserId = userId,
                    TagId = firstTag,
                };
                UsersTag secondUserTag = new UsersTag()
                {
                    UserId = userId,
                    TagId = secondTag,
                };
                context.UsersTags.Add(firstUserTag);
                context.UsersTags.Add(secondUserTag);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("remove/{userId}")]
        public ActionResult<IEnumerable<UsersTag>> RemoveTags(int userId)
        {
            try
            {
                var selectedUserTags = context.UsersTags.Where(x => x.UserId == userId).ToList();
                foreach (var item in selectedUserTags)
                {
                    context.UsersTags.Remove(item);
                    context.SaveChanges();
                }
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
