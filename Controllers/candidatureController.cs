using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using dot_net.Entities;
using dot_net.Models;
using dot_net.Services;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;
namespace dot_net.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    public class candidatureController : ControllerBase
    {
        private IWebHostEnvironment _hostEnvironment;
        public candidatureController(IWebHostEnvironment environment)
        {
            _hostEnvironment = environment;
        }

        [HttpPost("new")]
        public IActionResult addcandidature([FromBody] CandidatureModel model)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(model);
            string filePath= Path.Combine(_hostEnvironment.WebRootPath, "candidatures/"+ model.firstname + model.lastname + ".json");
            try{
                System.IO.File.WriteAllText(filePath, json);
                return Ok();
            }catch{
                return StatusCode(500);
            }
        }
    }
}