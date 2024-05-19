using Backend_InkSketch.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_InkSketch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        public static InkSketchDbContext context = new InkSketchDbContext();

        [HttpGet]
        [Route("get/data/{phone}")]
        public ActionResult<IEnumerable<User>> GetData(string phone)
        {
            try
            {
                var user = context.Users.Where(x => x.Phone == phone).FirstOrDefault();
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("get/{phone}")]
        public ActionResult<IEnumerable<User>> Get(string phone)
        {
            try
            {
                var user = context.Users.Where(x => x.Phone == phone).FirstOrDefault();
                if (user != null)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("enter/{phone}/{password}")]
        public ActionResult<IEnumerable<User>> Enter(string phone, string password)
        {
            try
            {
                var user = context.Users.Where(x => x.Phone == phone && x.Password == password).FirstOrDefault();
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Неверный пароль!");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpPost]
        [Route("reg")]
        public ActionResult<IEnumerable<User>> RegistrateUser([FromBody] User user)
        {
            try
            {
                var checkAvail = context.Users.Where(x => x.Phone == user.Phone).FirstOrDefault();
                if (checkAvail == null)
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    return Ok(context.Users.Where(x => x.Phone == user.Phone).FirstOrDefault());
                }
                else
                {
                    return BadRequest("Пользователь с таким номером уже есть");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpPut]
        [Route("name/{userId}/{result}")]
        public ActionResult<IEnumerable<User>> ChangeName(int userId, string result)
        {
            try
            {
                var selectedUser = context.Users.Where(x => x.UserId == userId).FirstOrDefault();
                selectedUser.Name = result;
                context.Entry(selectedUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
