using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using dot_net.Entities;
using dot_net.Models;
using dot_net.Services;
using dot_net.Requests;
using dot_net.Data;



namespace dot_net.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private DataContext _dbContext;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post ([FromBody]CreateUserRequest request)
        {
            var user = new User
            {
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = request.Role
            };
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return Ok();

        }

        [Authorize(Roles  = Role.Evaluator)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users =  await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // only allow evaluators to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Evaluator))
                return Forbid();

            var user =  _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        
        [HttpGet("evaluators")]
        public async Task<IActionResult> GetEvaluators()
        {
            var users =  await _userService.GetEvaluators();
            return Ok(users);
        }
    }
}