using Backend_InkSketch.ApplicationData;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_InkSketch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesViewController : Controller
    {
        public static InkSketchDbContext context = new InkSketchDbContext();
        private readonly IWebHostEnvironment _environment;
        public ImagesViewController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [HttpGet]
        [Route("get")]
        public ActionResult<IEnumerable<ImagesView>> Get()
        {
            try
            {
                var data = context.ImagesViews.ToList();
                return data.ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("get/random")]
        public ActionResult<IEnumerable<ImagesView>> GetRandom()
        {
            try
            {
                List<ImagesView> randomImages = new List<ImagesView>();
                var tagsList = context.Tags.ToList();
                foreach (var item in tagsList)
                {
                    var imagesWithItemTag = context.ImagesViews.Where(x => x.TagId == item.TagId).ToList();
                    Random random = new Random();
                    var randomSelectedImage = imagesWithItemTag.ElementAt(random.Next(1,imagesWithItemTag.Count));
                    randomImages.Add(randomSelectedImage);
                }
                return randomImages;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("get/tag/{tagId}")]
        public ActionResult<IEnumerable<ImagesView>> GetByTag(int tagId)
        {
            try
            {
                var selectedImages = context.ImagesViews.Where(x => x.TagId == tagId).ToList();
                return selectedImages;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("get/rec/{userId}/{skipCount}/{categoryId}")]
        public async Task<IEnumerable<ImagesView>> GetRec(int userId, int skipCount, int categoryId)
        {
            try
            {
                var enteredUser = context.Users.Where(x => x.UserId == userId).FirstOrDefault();
                if (enteredUser != null)
                {
                    if (categoryId != 0)
                    {
                        var userTags = context.UsersTags.Where(x => x.UserId == enteredUser.UserId).ToList();
                        if (userTags.Count > 1)
                        {
                            var data = await context.ImagesViews.Where(x => x.CategoryId == categoryId && x.TagId == userTags.Select(x => x.TagId).FirstOrDefault()).Skip(skipCount / 2).Take(2).ToListAsync();
                            data.AddRange(await context.ImagesViews.Where(x => x.CategoryId == categoryId && x.TagId == userTags.Select(x => x.TagId).LastOrDefault()).Skip(skipCount / 2).Take(2).ToListAsync());
                            return data;
                        }
                        else if (userTags.Count == 1)
                        {
                            var data = await context.ImagesViews.Where(x => x.TagId == userTags.Select(x => x.TagId).FirstOrDefault()).Skip(skipCount).Take(4).ToListAsync();
                            return data;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        var userTags = context.UsersTags.Where(x => x.UserId == enteredUser.UserId).ToList();
                        if (userTags.Count > 1)
                        {
                            var data = await context.ImagesViews.Where(x => x.TagId == userTags.Select(x => x.TagId).FirstOrDefault()).Skip(skipCount / 2).Take(2).ToListAsync();
                            data.AddRange(await context.ImagesViews.Where(x => x.TagId == userTags.Select(x => x.TagId).LastOrDefault()).Skip(skipCount / 2).Take(2).ToListAsync());
                            return data;
                        }
                        else if (userTags.Count == 1)
                        {
                            var data = await context.ImagesViews.Where(x => x.TagId == userTags.Select(x => x.TagId).FirstOrDefault()).Skip(skipCount).Take(4).ToListAsync();
                            return data;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        //[HttpPost]
        //[Route("get/image/{imageName}")]
        //public async Task<IActionResult> GetImage(string imageName)
        //{

        //    Byte[] b;
        //    b = await System.IO.File.ReadAllBytesAsync(Path.Combine(_environment.ContentRootPath, "Content", $"{imageName}"));
        //    return File(b, "image/jpeg");
        //}
    }
}
