using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using dot_net.Entities;
using dot_net.Models;
using dot_net.Services;
using dot_net.Data;

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

        [HttpPost("add")]
        public async Task<IActionResult> AddEvaluator([FromBody] User User)
        {
            // Needs role based authorization (admin)
            var userEntity = await _userService.AddEvaluator(User);
            return Ok(userEntity);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Needs role based authorization (admin)
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Needs role based authorization (admin)
            var user = _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("evaluators")]
        public async Task<IActionResult> GetEvaluators()
        {
            var users = await _userService.GetEvaluators();
            return Ok(users);
        }
    }
}