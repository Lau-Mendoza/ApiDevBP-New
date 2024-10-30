using ApiDevBP.Entities;
using ApiDevBP.Models;
using Microsoft.AspNetCore.Mvc;
using SQLite;
using System.Reflection;
using ApiDevBP.Services;
using Microsoft.Extensions.Options;

namespace ApiDevBP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        //private readonly  SQLiteConnection _db;

        private readonly UserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, IOptions<DataBaseConfig> config)
        {
            _logger = logger;
            string dbPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), config.Value.DbPath);

            _userService = new UserService(dbPath);
            //_db = new SQLiteConnection(localDb);
            //_db.CreateTable<UserEntity>();
        }

        [HttpPost]
        public IActionResult SaveUser(UserModel user)
        {
            //var result = _db.Insert(new UserEntity()
            //{
            //    Name = user.Name,
            //    Lastname = user.Lastname
            //});

            var result = _userService.CreateUser(user);

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userService.GetAllUsers();//_db.Query<UserEntity>($"Select * from Users");
            if (users != null)
            {
                //return Ok(users.Select(x=> new UserModel()
                //{
                //    Name = x.Name,
                //    Lastname = x.Lastname
                //}));
                return Ok(users);
            }
            return NotFound();
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateUser(int id, UserModel user)
        {
            var result= _userService.UpdateUser(id, user);  
            if (result) { return Ok(1); }
            else { return NotFound(); }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser (int id)
        {
            var result = _userService.DeleteUser(id);
            if (result) { return Ok(1); }
                else { return NotFound(); }
        } 

    }
}
