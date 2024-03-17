using Backend_InkSketch.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_InkSketch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : Controller
    {
        public static InkSketchDbContext context = new InkSketchDbContext();
        [HttpGet]
        [Route("add/{userId}/{imageId}")]
        public ActionResult<IEnumerable<FavoritesImage>> Add(int userId, int imageId)
        {
            try
            {
                FavoritesImage image = new FavoritesImage()
                {
                    ImageId = imageId,
                    UserId = userId,
                };
                context.FavoritesImages.Add(image);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("remove/{userId}/{imageId}")]
        public ActionResult<IEnumerable<FavoritesImage>> Remove(int userId, int imageId)
        {
            try
            {
                var selectedImage = context.FavoritesImages.Where(x => x.UserId == userId && x.ImageId == imageId).FirstOrDefault();
                context.FavoritesImages.Remove(selectedImage);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
