using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using dot_net.Entities;
using dot_net.Models;
using dot_net.Services;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
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
            // temporary
            string json = System.Text.Json.JsonSerializer.Serialize(model);
            return Ok();
            // string filePath= Path.Combine(_hostEnvironment.WebRootPath, "candidatures/"+ model.firstname + model.lastname + ".json");
            // try{
            //     System.IO.File.WriteAllText(filePath, json);
            //     return Ok();
            // }catch{
            //     return StatusCode(500);
            // }
        }
        
        [HttpPost("justificative")] 
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadJustificative() {
            var postedFile = Request.Form.Files["justificative"];
            if (postedFile != null){ 
                string fileExtension = Path.GetExtension(postedFile.FileName);
                Guid guid = Guid.NewGuid(); 
                string fileName = guid + fileExtension; 
                string filePath = Path.Combine(_hostEnvironment.WebRootPath, "justificatives/" + fileName);
                using (var stream = new FileStream(filePath, FileMode.Create)){ 
                    await postedFile.CopyToAsync(stream);
                    return Ok(fileName);
                }
            }      
            return StatusCode(400);
        }
    }
}