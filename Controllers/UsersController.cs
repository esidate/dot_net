using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using dot_net.Entities;
using dot_net.Models;
using dot_net.Services;
using dot_net.Data;
using Microsoft.AspNetCore.Authorization;
using System;

namespace dot_net.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService, DataContext dataContext)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPost("add")]
        public async Task<IActionResult> AddEvaluator([FromBody] User User)
        {
            var userEntity = await _userService.AddEvaluator(User);
            if (userEntity == null)
                return BadRequest(new { message = "Nom d'utilisateur déjà utilisé" });
            else
                return Ok(userEntity);
        }

        [Authorize]
        [HttpPost("modify")]
        public IActionResult modifyUser([FromBody] ModifyUserModel model)
        {
            var res = _userService.modifyUser(model);
            switch(res){
                case 0 : 
                    return BadRequest(new { message = "Compte introuvable" });
                case 1:
                    return BadRequest(new { message = "Nom d'utilisateur déjà utilisé" });
                case 2:
                    return Ok();
            }
            return BadRequest();

        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpGet("evaluators")]
        public async Task<IActionResult> GetEvaluators()
        {
            var users = await _userService.GetEvaluators();
            return Ok(users);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpGet("evaluators/block/{id}")]
        public IActionResult toggleEvaluatorsBlock(int id)
        {
            if (_userService.toggleEvaluatorsBlock(id))
                return Ok();
            else
                return NotFound();

        }

        [HttpGet("seed")]
        public string seedUsers()
        {
            var userFound = _userService.GetById(1);

            if (userFound == null)
            {
                var username = Environment.GetEnvironmentVariable("ADMIN_USERNAME");
                var firstname = Environment.GetEnvironmentVariable("ADMIN_FIRSTNAME");
                var lastname = Environment.GetEnvironmentVariable("ADMIN_LASTNAME");
                var password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");
                if (username != null && password != null)
                {
                    var user = new User { Id = 1, Username = username, FirstName = firstname, LastName = lastname, Role = "Admin", Password = password, Blocked = false };
                    _userService.AddUser(user);
                    return "1";
                }
                return "-1";
            }
            return "0";
        }
    }
}